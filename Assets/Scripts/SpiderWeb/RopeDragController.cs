using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopeDragController : MonoBehaviour
{
    public RopeVerlet rope;

    [Header("Slingshot Settings")]
    public float pickRadius = 0.3f;     // Радиус выбора сегмента
    public float maxStretch = 2f;       // Максимальное натяжение
    public float angleLimit = 65f;      // Максимальный угол вниз (чтобы не уводить под верёвку)

    private Vector2 dragStartPoint;

    private void Start()
    {
        rope = GetComponent<RopeVerlet>();
    }

    void Update()
    {
        Vector3 mouseWorld = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseWorld.z = 0;

        if (Input.GetMouseButtonDown(0))
            TryPickSegment(mouseWorld);

        if (Input.GetMouseButton(0) && rope.isDragging)
            UpdateDragging(mouseWorld);

        if (Input.GetMouseButtonUp(0) && rope.isDragging)
            ReleaseDragging(mouseWorld);
    }

    // --------------------------
    //  НАЧАЛО ПЕРЕТАСКИВАНИЯ
    // --------------------------
    void TryPickSegment(Vector3 mouseWorld)
    {
        float closestDist = pickRadius;
        int closestIndex = -1;

        for (int i = 1; i < rope.ropeSegments.Count - 1; i++)
        {
            float dist = Vector2.Distance(mouseWorld, rope.ropeSegments[i].CurrentPos);
            if (dist < closestDist)
            {
                closestDist = dist;
                closestIndex = i;
            }
        }

        if (closestIndex != -1)
        {
            rope.isDragging = true;
            rope.draggedSegmentIndex = closestIndex;
            dragStartPoint = rope.ropeSegments[closestIndex].CurrentPos;

            rope.StartDragging(closestIndex);
        }
    }


    // ------------------------
    //      ПРОЦЕСС ТЯГИ
    // ------------------------
    void UpdateDragging(Vector3 mouseWorld)
    {
        Vector2 segmentPos = rope.ropeSegments[rope.draggedSegmentIndex].CurrentPos;

        Vector2 rawDirection = mouseWorld - (Vector3)segmentPos;

        // Ограничение угла — не тянем слишком вниз
        float angle = Vector2.Angle(Vector2.up, -rawDirection);

        if (angle > angleLimit)
        {
            rawDirection = Quaternion.Euler(0, 0, angleLimit - angle) * rawDirection;
        }

        // Ограничение длины
        if (rawDirection.magnitude > maxStretch)
            rawDirection = rawDirection.normalized * maxStretch;

        Vector2 newPos = segmentPos + rawDirection;

        rope.dragPosition = newPos;
    }


    // ------------------------
    //       ОТПУСКАНИЕ
    // ------------------------
    void ReleaseDragging(Vector3 mouseWorld)
    {
        Vector2 segmentPos = rope.ropeSegments[rope.draggedSegmentIndex].CurrentPos;

        // сила выстрела — от точки сегмента к финальной точке мыши
        Vector2 force = (segmentPos - (Vector2)mouseWorld) * 2f;

        rope.isDragging = false;
        rope.ReleaseDragging(force);

        rope.draggedSegmentIndex = -1;
    }
}

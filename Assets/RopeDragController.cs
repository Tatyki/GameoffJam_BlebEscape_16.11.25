using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopeDragController : MonoBehaviour
{
    public RopeVerlet rope;
    public float pickRadius = 0.3f; 
    public float maxStretch = 2f;


    private void Start()
    {
        rope = GetComponent<RopeVerlet>();
    }
    void Update()
    {
        Vector3 mouseWorld = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseWorld.z = 0;

        if (Input.GetMouseButtonDown(0))
        {
            TryPickSegment(mouseWorld);
        }

        if (Input.GetMouseButton(0) && rope.isDragging)
        {
            rope.dragPosition = mouseWorld;
        }

        if (Input.GetMouseButtonUp(0))
        {
            rope.isDragging = false;
            rope.draggedSegmentIndex = -1;
            rope.ReleaseDragging();
        }

        if (rope.isDragging)
        {
            Vector2 grabbedPos = rope.ropeSegments[rope.draggedSegmentIndex].CurrentPos;

            Vector2 direction = mouseWorld - (Vector3)grabbedPos;
            float distance = direction.magnitude;

            if (distance > maxStretch)
            {
                direction = direction.normalized * maxStretch;
            }

            Vector2 newPos = grabbedPos + direction;

            if (newPos.y > grabbedPos.y)
                newPos.y = grabbedPos.y;

            rope.dragPosition = newPos;
        }
    }

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
            rope.StartDragging(closestIndex);
        }
    }
}

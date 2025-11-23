using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawWebUI : MonoBehaviour
{
    private RopeVerlet rope;
    private bool isAttached = false;
    private bool effectTriggered = false;

    private Vector3 startPosition;   // исходная точка объекта

    [Header("Follow Settings")]
    public float followAmount = 0.3f;        // Насколько сильно объект тянется за паутиной
    public float returnSpeed = 6f;           // Скорость возврата в исходное положение
    public float maxOffsetDistance = 0.5f;   // Максимальная дистанция смещения

    private void Start()
    {
        startPosition = transform.position;
    }

    private void Update()
    {
        // Узнать активную паутину
        rope = DragAndDrop.activeSpider != null
            ? DragAndDrop.activeSpider.activeRope
            : null;

        // Если паутина исчезла или очищается  сразу вернуть объект
        if (rope.endAttached == false)
        {
            ResetState();
            return;
        }

        // Проверяем прикрепление к ЭТОМУ объекту
        if (!isAttached &&
            rope.endAttached &&
            rope.ropeEndPoint == transform)
        {
            isAttached = true;
            TriggerEffect();
        }

        // Следовать за паутиной
        if (isAttached)
            FollowRope();
    }

    private void TriggerEffect()
    {
        if (effectTriggered) return;
        effectTriggered = true;

        Debug.Log("Эффект! Паутина прикрепилась к объекту " + gameObject.name);

        // Здесь ваш эффект
        // anim.SetTrigger("Activate");
        // SpawnEnemy();
        // PlaySound();
    }

    private void FollowRope()
    {
        if (rope == null || rope.ropeSegments.Count < 2) return;

        Vector2 endPos = rope.ropeSegments[rope.ropeSegments.Count - 1].CurrentPos;
        Vector2 pull = endPos - (Vector2)startPosition;

        // Смещение — часть натяжения
        Vector3 offset = pull * followAmount;

        // ---------- Ограничение радиуса ----------
        if (offset.magnitude > maxOffsetDistance)
            offset = offset.normalized * maxOffsetDistance;
        // ------------------------------------------

        Vector3 targetPos = startPosition + offset;

        transform.position = Vector3.Lerp(
            transform.position,
            targetPos,
            Time.deltaTime * returnSpeed
        );
    }

    private void ResetState()
    {
        // Мягко возвращаем объект в исходную позицию
        transform.position = Vector3.Lerp(
            transform.position,
            startPosition,
            Time.deltaTime * returnSpeed
        );

        isAttached = false;
        effectTriggered = false;
    }
}


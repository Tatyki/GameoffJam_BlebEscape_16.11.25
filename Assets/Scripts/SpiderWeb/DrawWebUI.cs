using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawWebUI : MonoBehaviour
{
    private RopeVerlet rope;
    private bool isAttached = false;
    private bool effectTriggered = false;
    public UIButtons thisUIButton;
    private blebLaunch BlebLaunch = null;
    private Coroutine blebCoroutine = null;
    public enum UIButtons
    {
        BlebLauncher,
        None
    }

    private Vector3 startPosition;   // исходная точка объекта

    [Header("Follow Settings")]
    public float followAmount = 0.3f;        // Насколько сильно объект тянется за паутиной
    public float returnSpeed = 6f;           // Скорость возврата в исходное положение
    public float maxOffsetDistance = 0.5f;   // Максимальная дистанция смещения

    private void Start()
    {
        startPosition = transform.position;
        switch (thisUIButton)
        {
            case UIButtons.BlebLauncher:
                BlebLaunch = GetComponent<blebLaunch>();
                break;
        }
    }

    private void Update()
    {
        //СТАРОЕ
        //if (DragAndDrop.activeSpider.activeRope != null)
        //{
        //    rope = DragAndDrop.activeSpider.activeRope;

        //    if (rope.endAttached == false)
        //    {
        //        return;
        //    }

        //    // Проверяем прикрепление к ЭТОМУ объекту
        //    if (!isAttached && rope.endAttached && rope.ropeEndPoint == transform)
        //    {
        //        isAttached = true;
        //        rope.WebCleared += ResetState;
        //        TriggerEffect();
        //    }

        //    // Следовать за паутиной
        //    if (isAttached)
        //        FollowRope();
        //}
        RopeVerlet activeRope = DragAndDrop.activeSpider?.activeRope;

        if (activeRope == null)
            return;

        rope = activeRope;


        if (!rope.endAttached)
        {
            return;
        }


        if (!isAttached && rope.ropeEndPoint == transform)
        {
            isAttached = true;
            rope.WebCleared += ResetState;
            TriggerEffect();
        }


        if (isAttached)
            FollowRope();
    }

    private void TriggerEffect()
    {
        if (effectTriggered) return;
        effectTriggered = true;
        Debug.Log("ваыф");
        // Здесь эффекты
        if (BlebLaunch != null)
        {
            blebCoroutine = StartCoroutine(BlebLaunch.LaunchBleb());
            StartCoroutine(BlebLaunch.SpawnMyha());
        }
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
        Debug.Log("СТОПЭ");
        StopCoroutine(blebCoroutine);
    }
        //СТАРОЕ
    //private void ResetState()
    //{
    //    // Мягко возвращаем объект в исходную позицию
    //    transform.position = Vector3.Lerp(
    //        transform.position,
    //        startPosition,
    //        Time.deltaTime * returnSpeed
    //    );

    //    isAttached = false;
    //    effectTriggered = false;
    //}
}


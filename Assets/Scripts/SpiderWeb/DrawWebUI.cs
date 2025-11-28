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
    private ExitButton exitButton;
    private Coroutine blebCoroutine = null;
    private Coroutine exitCoroutine = null;
    public enum UIButtons
    {
        BlebLauncher,
        ExitButton,
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
            case UIButtons.ExitButton:
                exitButton = GetComponent<ExitButton>();
                break;
        }
    }

    private void Update()
    {

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
        // Effect here
        if (BlebLaunch != null)
        {
            blebCoroutine = StartCoroutine(BlebLaunch.LaunchBleb());
            StartCoroutine(BlebLaunch.SpawnMyha());
        }
        if (exitButton!= null)
        {
            exitCoroutine = StartCoroutine(exitButton.FillSlider());
        }
    }

    private void FollowRope()
    {
        if (rope == null || rope.ropeSegments.Count < 2) return;

        Vector2 endPos = rope.ropeSegments[rope.ropeSegments.Count - 1].CurrentPos;
        Vector2 pull = endPos - (Vector2)startPosition;


        Vector3 offset = pull * followAmount;


        if (offset.magnitude > maxOffsetDistance)
            offset = offset.normalized * maxOffsetDistance;


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

        if(blebCoroutine != null)
        {
            StopCoroutine(blebCoroutine);
            blebCoroutine = null;
        }
        if (exitCoroutine != null)
        {
            StopCoroutine(exitCoroutine);
            StartCoroutine(exitButton.UnfillSlider());
            exitCoroutine = null;
        }

        effectTriggered = false;
        isAttached = false;
        if (rope != null)
        {
            rope.WebCleared -= ResetState;
        }
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


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
        PlayButton,
        CreditsButton,
        CloseButton,
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

    public void TriggerEffect()
    {
        if (effectTriggered) return;
        effectTriggered = true;
        // Effect here
        switch (thisUIButton)
        {
            case UIButtons.PlayButton:
                StartCoroutine(LoadComic());
                break;
            case UIButtons.CreditsButton:
                StartCoroutine(LoadCredits());
                break;
            case UIButtons.CloseButton:
                StartCoroutine(CloseGame());
                break;
        }
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

    IEnumerator LoadComic()
    {
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene("Comic");
    }
    IEnumerator LoadCredits()
    {
        yield return new WaitForSeconds(0.5f);
        ManagerOfScenes.TryLoadMainMenu();
    }
    IEnumerator CloseGame()
    {
        yield return new WaitForSeconds(0.5f);
        Application.Quit();
    }
}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExitButton : MonoBehaviour
{
    [Header("UI")]
    public Slider holdSlider = null;
    public CanvasGroup sliderCanvas = null;

    [Header("Settings")]
    public float fillTime = 2f;       
    public float fadeSpeed = 4f;       


    private void Start()
    {
        holdSlider.value = 0f;
        sliderCanvas.alpha = 0f;
    }

    public IEnumerator FillSlider()
    {
        while (sliderCanvas.alpha < 1f)
        {
            sliderCanvas.alpha += Time.deltaTime * fadeSpeed;
            yield return null;
        }

        // заполняем полосу
        while (holdSlider.value < 1f)
        {
            holdSlider.value += Time.deltaTime / fillTime;
            yield return null;
        }

        ManagerOfScenes.TryLoadMainMenu();
    }

    public IEnumerator UnfillSlider()
    {
        while (holdSlider.value > 0f)
        {
            holdSlider.value -= Time.deltaTime;
            yield return null;
        }

        while (sliderCanvas.alpha > 0f)
        {
            sliderCanvas.alpha -= Time.deltaTime * fadeSpeed;
            yield return null;
        }
        StopCoroutine(UnfillSlider());
    }

    public void ExitToMainMenu()
    {
        ManagerOfScenes.TryLoadMainMenu();
    }
}

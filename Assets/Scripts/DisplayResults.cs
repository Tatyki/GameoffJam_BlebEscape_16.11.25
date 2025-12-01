using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayResults : MonoBehaviour
{
    public GameObject[] resultTexts; // массив объектов с текстом
    public float delay = 0.5f;       // задержка между появлениями
    public bool StartFromAwake;

    void Start()
    {
        foreach (GameObject textObj in resultTexts)
        {
            textObj.SetActive(false);
        }
        if (StartFromAwake)
        {
            Display();
        }
    }

    IEnumerator ShowResultsSequentially()
    {
        foreach (GameObject textObj in resultTexts)
        {
            yield return new WaitForSeconds(delay);
            textObj.SetActive(true);
            SoundManager.PlaySound(SoundType.UI);
        }
    }

    public void Display()
    {

        // Запускаем появление текста
        StartCoroutine(ShowResultsSequentially());
    }
}

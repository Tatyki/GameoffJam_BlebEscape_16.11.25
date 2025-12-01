using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Reflection;

public class ComicController : MonoBehaviour
{
    public ComicClass realComic;
    static ComicClass comics;

    //Pages[] currentPages;

    public Image realImage;
    static Image image;

    public TextMeshProUGUI textMeshPro;

    static int pagesIndex;
    bool once;

    //string level = "LevelOne";


    void Start()
    {
        //currentPages = comics.pages;
        image = realImage;
        comics = realComic;

        pagesIndex = 0;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //pagesIndex ++;
            //SetComic(1);
        }
    }

    public void SetComic(int index)
    {
        if (pagesIndex != realComic.pages.Length)
        {
            if (textMeshPro != null) textMeshPro.text = comics.pages[pagesIndex].imageText;
            if (realImage != null) realImage.sprite = comics.pages[pagesIndex].sprite;
            pagesIndex += index;
        }
        else if (!once)
        {
            once = true;
            pagesIndex = 0;
            Debug.Log("done111111111111111111111111111");
            //StartScene.toChange = true;
            //StartScene.sceneName = level;
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuSpider : MonoBehaviour
{
    public DisplayResults display;
    private Animator anim;
    public GameObject text;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            display.Display();
            anim.Play("MainMenuSpiderMovesAway");
            Destroy(text);
        }
    }
}

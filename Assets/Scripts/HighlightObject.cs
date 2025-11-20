using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighlightObject : MonoBehaviour
{
    private Animator anim;
    private Camera cam;
    void Start()
    {
        cam = Camera.main;
        anim = GetComponent<Animator>();
    }


    void Update()
    {
        var hit = Physics2D.Raycast(cam.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
        if (hit && hit.collider.gameObject == gameObject)
        {
            anim.SetBool("Highlighted", true);
        }
        else
        {
            anim.SetBool("Highlighted", false);
        }
    }
}

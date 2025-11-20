using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragAndDrop : MonoBehaviour
{
    [SerializeField] private bool isDragging = false;
    private Camera cam;
    private Rigidbody2D rb;
    public RopeVerlet ropePrefab;
    public Animator anim;
    public bool isActive;
    public bool canMove = true;
    public static DragAndDrop activeSpider;
    public RopeVerlet activeRope;

    private void Start()
    {
        cam = Camera.main;
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        var hit = Physics2D.Raycast(cam.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
        if (hit && hit.collider.gameObject == gameObject)
        {
            anim.SetBool("Highlighted", true);
        } else
        {
            anim.SetBool("Highlighted", false);
        }
        if (Input.GetMouseButtonDown(1))
        {
            
            if (hit && hit.collider.gameObject == gameObject)
            {
                if (activeSpider != null && activeSpider != this)
                {
                    activeSpider.isActive = false;
                    activeSpider.rb.isKinematic = true;
                }

                isActive = true;
                activeSpider = this;
                activeSpider.rb.isKinematic = false;
                isDragging = true;
            }

        } else if(Input.GetMouseButtonUp(1))
        {
            isDragging = false;
        }

        if (isDragging && canMove)
        {
            anim.SetBool("Highlighted", true);
            rb.MovePosition((Vector2)cam.ScreenToWorldPoint(Input.mousePosition));
        }
        
        if (isActive)
        {
            anim.SetBool("isActive", true);
        }
        else
        {
            anim.SetBool("isActive", false);
        }
    }
    public void ShootWeb(Transform startPos,Transform endPos)
    {
        if (startPos != endPos && activeRope == null)
        {
            RopeVerlet newRope = Instantiate(ropePrefab);
            newRope.ropeStartPoint = startPos;
            newRope.ropeEndPoint = endPos;
            newRope.ReleaseWeb();
            activeRope = newRope;
        }
    }


}

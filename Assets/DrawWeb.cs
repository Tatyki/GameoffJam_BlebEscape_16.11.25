using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawWeb : MonoBehaviour
{
    public bool ObjectIsCharged;
    public bool PeriodicCharge;
    public float ChargeTime;
    public DragAndDrop isSpiderCharged;
    private Animator anim;
    private void Start()
    {
        anim = GetComponent<Animator>();
        if (PeriodicCharge)
        {
            StartCoroutine(Charge());
        }
        TryGetComponent<DragAndDrop>(out isSpiderCharged);
        
    }
    private void OnMouseDown()
    {
        if(DragAndDrop.activeSpider != null)
        {
            DragAndDrop.activeSpider.ShootWeb(DragAndDrop.activeSpider.gameObject.transform,transform);
        }

    }
    IEnumerator Charge()
    {
        while (true)
        {
            ObjectIsCharged = false;
            anim.SetBool("Charged", false);
            yield return new WaitForSeconds(ChargeTime);
            ObjectIsCharged = true;
            anim.SetBool("Charged", true);
            yield return new WaitForSeconds(ChargeTime);
        }
    }
    private void Update()
    {
        if (isSpiderCharged != null && !isSpiderCharged.canMove)
        {
            ObjectIsCharged = true;
        }
        else if (isSpiderCharged != null && isSpiderCharged.canMove)
        {
            ObjectIsCharged = false;
        }
    }
}

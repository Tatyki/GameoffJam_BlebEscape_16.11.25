using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargedWeb : MonoBehaviour
{
    public RopeVerlet rope;
    public bool isCharged;
    private bool wasChargedLastFrame = false;
    private LineRenderer lineRenderer;
    private DragAndDrop ownerSpider; 

    private void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        rope = GetComponent<RopeVerlet>();

        FindOwnerSpider();
    }

    private void FindOwnerSpider()
    {
        DragAndDrop[] allSpiders = FindObjectsOfType<DragAndDrop>();
        foreach (DragAndDrop spider in allSpiders)
        {
            if (spider.activeRope == this.rope)
            {
                ownerSpider = spider;
                break;
            }
        }
    }

    private void Update()
    {
        bool previousChargeState = isCharged;
        isCharged = CheckChargeStatus();

        if (ownerSpider == null)
        {
            FindOwnerSpider(); 
        }

        if (ownerSpider != null)
        {
            ownerSpider.canMove = !isCharged;
        }

        if (isCharged)
        {
            lineRenderer.startColor = Color.blue;
            lineRenderer.endColor = Color.blue;
        }
        else
        {
            lineRenderer.startColor = Color.white;
            lineRenderer.endColor = Color.white;
        }

        if (wasChargedLastFrame && !isCharged)
        {
            UpdateConnectedWebs();
        }

        wasChargedLastFrame = isCharged;
    }

    private bool CheckChargeStatus()
    {
        if (rope.ropeEndPoint == null || !rope.endAttached)
            return false;

        DrawWeb endDrawWeb = rope.ropeEndPoint.gameObject.GetComponent<DrawWeb>();
        if (endDrawWeb != null && endDrawWeb.ObjectIsCharged)
            return true;

        ChargedWeb connectedWeb = rope.ropeEndPoint.gameObject.GetComponent<ChargedWeb>();
        if (connectedWeb != null && connectedWeb.isCharged)
            return true;

        return false;
    }

    private void UpdateConnectedWebs()
    {
        foreach (ChargedWeb web in FindObjectsOfType<ChargedWeb>())
        {
            if (web != this && web.rope.ropeEndPoint != null &&
                web.rope.ropeEndPoint.gameObject == this.gameObject)
            {
                web.isCharged = web.CheckChargeStatus();
            }
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if(isCharged && collision.gameObject.CompareTag("bleb"))
        {
            Destroy(collision.gameObject);
        }
    }
}

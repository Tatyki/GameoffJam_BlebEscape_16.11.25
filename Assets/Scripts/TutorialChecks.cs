using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialChecks : MonoBehaviour
{
    //Spider is active?
    public bool isSpiderActive()
    {
        bool spiderIsActive = false;
        if (DragAndDrop.activeSpider != null)
        {
            spiderIsActive = true;
        }
        return spiderIsActive;
    }

    [HideInInspector]public Vector3 lastPos;
    [HideInInspector]public float accumulated = 0f;
    //Spider is Moving?
    public bool isSpiderMoved()
    {
        var spider = DragAndDrop.activeSpider;
        if (spider == null) { accumulated = 0f; return false; }

        if (lastPos == Vector3.zero) { lastPos = spider.transform.position; return false; }

        accumulated += Vector3.Distance(spider.transform.position, lastPos);
        lastPos = spider.transform.position;

        if (accumulated >= 1.5f) { accumulated = 0f; return true; }
        return false;
    }

    //Web is Released?
    public bool isWebReleased()
    {
        if(DragAndDrop.activeSpider == null)
        {
            return false;
        }
        if (DragAndDrop.activeSpider.activeRope != null && DragAndDrop.activeSpider.activeRope.releaseWeb)
        {
            return true;
        }
        return false;
    }

    public BlebInBunker blebInBunker;
    //Bleb is Saved?
    private bool isBlebSaved()
    {
        if(blebInBunker.blebInBunkerCount >= 1f)
        {
            return true;
        }
        return false;
    }

    //Web is Cleared?
    public bool isWebCleared()
    {
        if (DragAndDrop.activeSpider == null)
        {
            return false;
        }
        if (DragAndDrop.activeSpider.activeRope != null && DragAndDrop.activeSpider.activeRope.endAttached == false)
        {
            return true;
        }
        return false;
    }

    private bool AllmyhaDead = false;
    //All Myha is reached LastPoint?
    public bool isMyhaReachedLastPoint()
    {
        if (AllmyhaDead) return true;
        var myhas = FindObjectsOfType<myha>();

        if (myhas.Length == 0)
        {
            AllmyhaDead = true;
            return true;
        }

        return false;
    }


}

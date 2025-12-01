using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialChecks : MonoBehaviour
{
    public GameObject spider;
    [HideInInspector] public Vector3 lastPos;
    /*[HideInInspector]*/ public float accumulated = 0f;

    bool wasMyhaEverSpawned;

    private void Start()
    {
        //var spider = FindAnyObjectByType<DragAndDrop>().gameObject;
        //if (spider != null)
        // lastPos = spider.transform.position;
        lastPos = spider.transform.position;
    }

    private void Update()
    {
        if(!wasMyhaEverSpawned)
        {
            var myhas = FindObjectsOfType<myha>();
            if (myhas != null && myhas.Length != 0)
            {
                wasMyhaEverSpawned = true ;
            }
        }
    }

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


    public bool isSpiderMoved()
    {
        var spider = DragAndDrop.activeSpider;
        if (spider == null)
        {
            accumulated = 0f;
            return false;
        }

        // рассто€ние от предыдущей позиции
        accumulated = Vector3.Distance(spider.transform.position, lastPos);

        // обновл€ем lastPos
        //lastPos = spider.transform.position;

        if (accumulated >= 3f)
        {
            //accumulated = 0f;
            return true;
        }

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
    public bool isBlebSaved()
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

        if (wasMyhaEverSpawned && myhas.Length == 0)
        {
            AllmyhaDead = true;
            return true;
        }

        return false;
    }


}

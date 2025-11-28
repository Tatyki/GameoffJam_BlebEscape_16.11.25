using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using Pathfinding;
using JetBrains.Annotations;

public class myha : MonoBehaviour
{
   [Header("Settings")]
    public float moveSpeed = 5f; 
    public float reachDistance = 0.2f;
    [Space]
    private Transform[] pathPoints;
    public Transform _path;
    
    private int currentPoint = 0;
    private Rigidbody2D rb;
    private AIPath path;

    [Header("Detection")]
    public float detectionRadius = 3.5f; 
    public float checkTime = 2.5f; 
    [Space]
    public LayerMask hitMask;
    public Transform lastPosition;  
    bool spotted = false;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        path = GetComponent<AIPath>();

        _path = GameObject.FindGameObjectWithTag("point").transform;

        if (_path != null)
        {
            int childCount = _path.childCount;
            pathPoints = new Transform[childCount];

            for (int i = 0; i < childCount; i++)
            {
                pathPoints[i] = _path.GetChild(i);
            }
        }

        lastPosition = GameObject.FindGameObjectWithTag("lastPosition").transform;

        path.maxSpeed = moveSpeed;
        path.canMove = false;
    }
    void Update()
    {
        blebFollow();

        if(!spotted && currentPoint < pathPoints.Length && pathPoints.Length > 0)
        {
            moving(pathPoints[currentPoint]);
            if(Vector2.Distance(transform.position, pathPoints[currentPoint].position) < reachDistance) currentPoint++;
        }
    }
    void blebFollow()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, detectionRadius);

        foreach (Collider2D hit in hits)
        {
            if (hit.CompareTag("bleb"))
            {
                Vector2 direction = (hit.transform.position - transform.position);
                RaycastHit2D _hit = Physics2D.Raycast(transform.position, direction, detectionRadius, hitMask);
                Debug.DrawRay(transform.position, direction * detectionRadius, Color.red);

                if(_hit.collider.CompareTag("bleb"))
                {
                    lastPosition.position = _hit.point;
                    CancelInvoke("unspotted");
                    spotted = true;
                }
            }
        }
        if(Vector2.Distance(transform.position, lastPosition.position) < reachDistance)
        {
            Invoke("unspotted",checkTime);
        }
        if (spotted) moving(lastPosition);
    }
    void unspotted()
    {
        spotted = false;
        closestPoint();
    }
    void moving(Transform target)
    {
        path.canMove = true;
        path.destination = target.position;
    }
    void closestPoint()
    {
        float minDistance = Vector2.Distance(transform.position, pathPoints[currentPoint].position);
        for (int i = 1; i < pathPoints.Length; i++)
        {
            float dist = Vector2.Distance(transform.position, pathPoints[i].position);
            if (dist < minDistance)
            {
                minDistance = dist;
                currentPoint = i;
            }
        }
    }
    void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
        Gizmos.DrawWireSphere(lastPosition.position, 0.5f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Death"))
        {
            Destroy(this.gameObject);
        }
    }
}

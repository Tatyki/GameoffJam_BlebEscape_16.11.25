using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopeVerlet : MonoBehaviour
{
    [Header("Rope")]
    [SerializeField] private int numOfRopeSegments = 35;
    [SerializeField] private float ropeSegmentLength = 0.11f;
    [SerializeField] public Transform ropeStartPoint;
    [SerializeField] public Transform ropeEndPoint;

    [Header("Physics")]
    [SerializeField] private Vector2 gravityForce = new Vector2(0f, -2f);
    [SerializeField] private float dampingFactor = 0.98f;
    public float launchForceMultiplier = 1.2f;

    [Header("Constraints")]
    [SerializeField] private int numOfConstraintsRuns = 50;

    private LineRenderer lineRenderer;
    private EdgeCollider2D edgeCollider;
    public List<RopeSegment> ropeSegments = new List<RopeSegment>();

    public class AttachedBody
    {
        public Rigidbody2D rb;
        public int nearestSegment;
    }
    public List<AttachedBody> attachedBodies = new List<AttachedBody>();

    private List<Vector2> baselinePositions = null; 
    public float minLaunchStretch = 0.1f;



    public bool releaseWeb;
    public bool endAttached;
    private float currentWidth = 0.09f;
    private float targetWidth = 0.09f;
    public bool isDragging = false;
    public int draggedSegmentIndex = -1;
    public Vector2 dragPosition;
    public System.Action WebCleared;


    [Header("Check for Dialogue")]
    public bool dialogueIsActive;
    
    public void ReleaseWeb()
    {
        ropeSegments.Clear();  
        attachedBodies.Clear();

        SoundManager.PlaySound(SoundType.WebShoot);
        releaseWeb = true;
        endAttached = true;
        edgeCollider = GetComponent<EdgeCollider2D>();
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.startWidth = 0.09f;
        lineRenderer.endWidth = 0.09f;
        targetWidth = 0.09f;
        lineRenderer.positionCount = numOfRopeSegments;
        Debug.Log(releaseWeb);

            //ropeStartPoint = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        for (int i = 0; i < numOfRopeSegments; i++)
        {
          ropeSegments.Add(new RopeSegment(ropeStartPoint.position));
          //ropeStartPoint.position.y -= ropeSegmentLength;
        }
       
    }

    private void Update()
    {
        if (releaseWeb)
        {
            DrawRope();
            
        }

        if (Mathf.Abs(currentWidth - targetWidth) > 0.001f)
        {
            currentWidth = Mathf.Lerp(currentWidth, targetWidth, Time.deltaTime * 4);
            lineRenderer.startWidth = currentWidth;
            lineRenderer.endWidth = currentWidth;
        }

        if (Input.GetKeyDown(KeyCode.Space) && !dialogueIsActive)
        {
            if (DragAndDrop.activeSpider != null &&
           DragAndDrop.activeSpider.activeRope == this) // проверка на активного
            {
                StartCoroutine(ClearWeb());
            }
        }
        
    }

    

    private void FixedUpdate()
    {
        if (releaseWeb)
        {
            Simulate();

            for (int i = 0; i < numOfConstraintsRuns; i++)
            {
                ApplyConstraints();
            }
        }
    }

    public void DrawRope()
    {
        Vector3[] ropePos = new Vector3[numOfRopeSegments];
        Vector2[] colliderPoints = new Vector2[ropeSegments.Count];

        for (int i = 0; i < ropeSegments.Count; i++)
        {
            ropePos[i] = ropeSegments[i].CurrentPos;
            colliderPoints[i] = ropeSegments[i].CurrentPos;
        }

        lineRenderer.SetPositions(ropePos);
        edgeCollider.points = colliderPoints;
    }

    private void Simulate()
    {
        for (int i = 0; i < ropeSegments.Count; i++)
        {
            RopeSegment segment = ropeSegments[i];
            Vector2 velocity = (segment.CurrentPos - segment.OldPos) * dampingFactor;

            segment.OldPos = segment.CurrentPos;
            segment.CurrentPos += velocity;
            segment.CurrentPos += gravityForce * Time.fixedDeltaTime;
            ropeSegments[i] = segment;

        }
    }

    

    public IEnumerator ClearWeb()
    {
        if(DragAndDrop.activeSpider.activeRope.releaseWeb)
        {

            DragAndDrop.activeSpider.activeRope.endAttached = false;
            DragAndDrop.activeSpider.activeRope.targetWidth = 0f;
            WebCleared?.Invoke();
            yield return new WaitForSeconds(0.5f);
            Destroy(gameObject);

        }
    }

    public IEnumerator ClearThisWeb()
    {
        RopeVerlet rope = this;

        if (rope.releaseWeb)
        {
            rope.endAttached = false;
            rope.targetWidth = 0f;
            WebCleared?.Invoke();
            yield return new WaitForSeconds(0.5f);
            Destroy(rope.gameObject);
        }
    }

    private void ApplyConstraints()
    {

        RopeSegment firstSegment = ropeSegments[0];
        firstSegment.CurrentPos = ropeStartPoint.position;
        ropeSegments[0] = firstSegment;

        if (endAttached)
        {
            RopeSegment endSegment = ropeSegments[numOfRopeSegments - 1];
            endSegment.CurrentPos = ropeEndPoint.position;
            ropeSegments[numOfRopeSegments - 1] = endSegment;
        }

        if (isDragging && draggedSegmentIndex >= 0 && draggedSegmentIndex < ropeSegments.Count)
        {
            RopeSegment seg = ropeSegments[draggedSegmentIndex];
            seg.CurrentPos = dragPosition;        
            ropeSegments[draggedSegmentIndex] = seg;
            
        }

        for (int i = 0; i < numOfRopeSegments -1; i++)
        {
            RopeSegment currentSegment = ropeSegments[i];
            RopeSegment nextSegment = ropeSegments[i + 1];

            float distance = (currentSegment.CurrentPos - nextSegment.CurrentPos).magnitude;
            float difference = (distance - ropeSegmentLength);

            Vector2 changeDir = (currentSegment.CurrentPos - nextSegment.CurrentPos).normalized;
            Vector2 changeVector = changeDir * difference;

            if (i != 0)
            {
                currentSegment.CurrentPos -= (changeVector * 0.5f);
                nextSegment.CurrentPos += (changeVector * 0.5f);
            }
            else
            {
                nextSegment.CurrentPos += changeVector;
            }

            ropeSegments[i] = currentSegment;
            ropeSegments[i + 1] = nextSegment;
        }

        if (attachedBodies.Count > 0)
        {
            for (int i = attachedBodies.Count - 1; i >= 0; i--)
            {
                var ab = attachedBodies[i];

                if (ab.rb == null)
                {
                    attachedBodies.RemoveAt(i);
                    continue;
                }

                int seg = Mathf.Clamp(ab.nearestSegment, 0, ropeSegments.Count - 1);
                float distance = Vector2.Distance(ropeSegments[seg].CurrentPos, ab.rb.position);


                if (distance > 0.7f)
                {
                    attachedBodies.RemoveAt(i);
                    continue;
                }

                float weight = ab.rb.mass * 0.005f;

                RopeSegment rs = ropeSegments[seg];
                rs.CurrentPos += Vector2.down * weight;
                ropeSegments[seg] = rs;
            }
        }

        int blebCount = 0;
        int myhaCount = 0;

        foreach (var ab in attachedBodies)
        {
            if (ab.rb == null) continue;

            if (ab.rb.CompareTag("bleb"))
            {
                blebCount++;
            }

            if (ab.rb.CompareTag("Myha"))
            {
                myhaCount++;
            }
        }

        if (blebCount >= 3 || myhaCount >= 5)
        {
            Debug.Log("Clear web");
            StartCoroutine(ClearWeb());
            return;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        

        Rigidbody2D rb = collision.collider.GetComponent<Rigidbody2D>();
        if (rb == null) return;

        
        foreach (var ab in attachedBodies)
            if (ab.rb == rb) return;

        
        int nearest = 0;
        float minDist = float.MaxValue;

        for (int i = 0; i < ropeSegments.Count; i++)
        {
            float dist = Vector2.Distance(ropeSegments[i].CurrentPos, rb.position);
            if (dist < minDist)
            {
                minDist = dist;
                nearest = i;
            }
        }

        attachedBodies.Add(new AttachedBody()
        {
            rb = rb,
            nearestSegment = nearest
        });

        if (rb.CompareTag("bleb"))
        {
            var blebParticles = rb.GetComponent<BlebParticleSystem>();
            if (blebParticles != null)
            {
                blebParticles.SetOnWeb(true);
            }
        }


    }

    public void LaunchAllAttached()
    {
        if (baselinePositions == null) return; 

        foreach (var ab in attachedBodies)
        {
            if (ab.rb == null) continue;

            int seg = Mathf.Clamp(ab.nearestSegment, 0, ropeSegments.Count - 1);

            
            Vector2 baselinePos = baselinePositions[seg];
            Vector2 currentPos = ropeSegments[seg].CurrentPos;
            Vector2 pull = baselinePos - currentPos; 
            float stretch = pull.magnitude;

            if (stretch < minLaunchStretch) continue; 

            Vector2 force = pull.normalized * stretch * launchForceMultiplier;

            ab.rb.velocity = Vector2.zero; 
            ab.rb.AddForce(force, ForceMode2D.Impulse);
        }

        foreach (var ab in attachedBodies)
        {
            if (ab.rb == null) continue;

            if (ab.rb.CompareTag("bleb"))
            {
                var particles = ab.rb.GetComponent<BlebParticleSystem>();
                if (particles != null)
                    particles.SetOnWeb(false);
            }
        }

        attachedBodies.Clear();
    }

    public void StartDragging(int segmentIndex)
    {
        isDragging = true;
        draggedSegmentIndex = segmentIndex;

        
        baselinePositions = new List<Vector2>(ropeSegments.Count);
        for (int i = 0; i < ropeSegments.Count; i++)
            baselinePositions.Add(ropeSegments[i].CurrentPos);
    }

    public void ReleaseDragging(Vector3 mouseWorld)
    {
        isDragging = false;
        draggedSegmentIndex = -1;

        
        LaunchAllAttached();

        
        baselinePositions = null;
    }


    public struct RopeSegment
    {
        public Vector2 CurrentPos;
        public Vector2 OldPos;

        public RopeSegment(Vector2 pos)
        {
            CurrentPos = pos;
            OldPos = pos;
        }
    }
}

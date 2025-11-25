using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighlightObject : MonoBehaviour
{
    [Header("Настройки")]
    [SerializeField] private GameObject[] highlightableObjects; // Список объектов для подсветки (прикрепите в инспекторе)

    private Animator[] animators; // Массив аниматоров для каждого объекта
    private Camera cam;
    private GameObject currentlyHighlighted; // Текущий подсвеченный объект (для оптимизации)

    void Start()
    {
        cam = Camera.main;

        int count = highlightableObjects.Length;
        animators = new Animator[count];
        for (int i = 0; i < count; i++)
        {
            if (highlightableObjects[i] == null)
            {
                
                continue;
            }
            animators[i] = highlightableObjects[i].GetComponent<Animator>();
        }
        currentlyHighlighted = null;
    }

    void Update()
    {
        if (currentlyHighlighted != null)
        {
            int prevIndex = GetObjectIndex(currentlyHighlighted);
            if (prevIndex >= 0 && animators[prevIndex] != null)
            {
                animators[prevIndex].SetBool("Highlighted", false);
            }
        }
        Vector2 mouseWorldPos = cam.ScreenToWorldPoint(Input.mousePosition);

        RaycastHit2D hit = Physics2D.Raycast(mouseWorldPos, Vector2.zero);

        if (hit.collider != null)
        {
            GameObject hitObject = hit.collider.gameObject;
            int hitIndex = GetObjectIndex(hitObject);

            if (hitIndex >= 0 && animators[hitIndex] != null)
            {
                animators[hitIndex].SetBool("Highlighted", true);
                currentlyHighlighted = hitObject;
                return;
            }
        }

        currentlyHighlighted = null;
    }

    private int GetObjectIndex(GameObject targetObject)
    {
        for (int i = 0; i < highlightableObjects.Length; i++)
        {
            if (highlightableObjects[i] == targetObject)
            {
                return i;
            }
        }
        return -1;
    }
}


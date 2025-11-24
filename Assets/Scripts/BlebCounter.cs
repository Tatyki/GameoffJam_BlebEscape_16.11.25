using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlebCounter : MonoBehaviour
{
    public static BlebCounter Instance;

    public int maxBlebs = 5;   // максимально доступно
    public int currentBlebs;   // сколько осталось

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        //currentBlebs = maxBlebs;
    }

    public void RemoveBlob()
    {
        currentBlebs = Mathf.Max(0, currentBlebs - 1);
    }
}

using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlebCounter : MonoBehaviour
{
    public static BlebCounter Instance;

    public int maxBlebs = 5;   // максимально доступно
    public int currentBlebs;   // сколько осталось
    public BlebData blebData;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        currentBlebs = blebData.blebCount;
    }

    public void RemoveBlob()
    {
        blebData.blebCount--;
        currentBlebs = blebData.blebCount;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadSecondLevel : MonoBehaviour
{
    public BlebData blebData;
    public BlebInBunker inBunker;
    void Awake()
    {
        blebData.blebCount = 5;
        inBunker.blebInBunkerCount = 0;
    }
}

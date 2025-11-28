using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadSecondLevel : MonoBehaviour
{
    public BlebData blebData;
    public BlebInBunker inBunker;
    void Start()
    {
        blebData.blebCount = 5;
        inBunker.blebInBunkerCount = 0;
    }
}

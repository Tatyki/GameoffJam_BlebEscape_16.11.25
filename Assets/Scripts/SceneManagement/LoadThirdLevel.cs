using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadThirdLevel : MonoBehaviour
{
    public BlebData blebData;
    public BlebInBunker inBunker;

    void Awake()
    {
        blebData.blebCount = inBunker.blebInBunkerCount;
    }
    void Start()
    {
        inBunker.blebInBunkerCount = 0;
    }
}

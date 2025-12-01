using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadFirstLevel : MonoBehaviour
{
    public BlebData blebData;
    public BlebInBunker inBunker;
    void Start()
    {
        blebData.blebCount = 3;
        inBunker.blebInBunkerCount = 0;
    }
}

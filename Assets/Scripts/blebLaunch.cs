using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class blebLaunch : MonoBehaviour
{
    public float Timer;
    public GameObject blebPrefab;
    public GameObject myhaPrefab;
    public BlebData blebData;
    public Transform blebSpawnPoint;
    public Transform myhaSpawnPoint;
    public IEnumerator LaunchBleb()
    {
        while (blebData.blebCount != 0)
        {
            blebData.blebCount--;
            Instantiate(blebPrefab, blebSpawnPoint);
            yield return new WaitForSeconds(Timer);
        }
    }

    public IEnumerator SpawnMyha()
    {
        while (true)
        {
            Instantiate(myhaPrefab, myhaSpawnPoint);
            yield return new WaitForSeconds(Timer);
        }
    }
}

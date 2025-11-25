using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class blebLaunch : MonoBehaviour
{
    public float timerBleb;
    public float timerMyha;

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
            yield return new WaitForSeconds(timerBleb);
        }
    }

    public IEnumerator SpawnMyha()
    {
        while (true)
        {
            yield return new WaitForSeconds(timerMyha);
            Instantiate(myhaPrefab, myhaSpawnPoint);
        }
    }
}

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
    private int blebLeft;
    public Transform blebSpawnPoint;
    public Transform myhaSpawnPoint;

    private void Start()
    {
        blebLeft = blebData.blebCount;
    }
    public IEnumerator LaunchBleb()
    {
        while (blebLeft != 0)
        {
            blebLeft--;
            Instantiate(blebPrefab, blebSpawnPoint);
            SoundManager.PlaySound(SoundType.BlebSpawn);
            yield return new WaitForSeconds(timerBleb);
        }
    }

    public IEnumerator SpawnMyha()
    {
        while (true)
        {
            yield return new WaitForSeconds(timerMyha);
            Instantiate(myhaPrefab, myhaSpawnPoint);
            SoundManager.PlaySound(SoundType.FlySpawn);
        }
    }
}

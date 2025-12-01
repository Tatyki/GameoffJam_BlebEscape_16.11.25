using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialSpawner : MonoBehaviour
{
    [SerializeField] Transform BlebSpawnPoint;
    [SerializeField] Transform myhaSpawnPoint;
    [SerializeField] GameObject myhaPrefab;
    [SerializeField] GameObject blebPrefab;

    public RopeVerlet webPrefab;

    public void SpawnBleb(int a)
    {
        Instantiate(blebPrefab, BlebSpawnPoint);
        SoundManager.PlaySound(SoundType.BlebSpawn);
        // spawn Bleb in BlebSpawnPoint
        Debug.Log("BLEB spawned"); // true
        // void can be activate again if Bleb died, instead entering the safe zone -- activate it in TutorialCheck
    }

    public void SpawnMyha(int a)
    {
        StartCoroutine(Myha());
    }

    IEnumerator Myha()
    {
        yield return new WaitForSeconds(3);
        Instantiate(myhaPrefab, myhaSpawnPoint);
        SoundManager.PlaySound(SoundType.FlySpawn);
        yield return new WaitForSeconds(0.5f);
        Instantiate(myhaPrefab, myhaSpawnPoint);
        SoundManager.PlaySound(SoundType.FlySpawn);
        yield return new WaitForSeconds(0.5f);
        Instantiate(myhaPrefab, myhaSpawnPoint);
        SoundManager.PlaySound(SoundType.FlySpawn);
        Debug.Log("MYHA spawned"); // true
    }

    public void FreezePlayer(int a)
    {
        webPrefab = FindAnyObjectByType<RopeVerlet>();
        if (webPrefab != null) webPrefab.dialogueIsActive = true;
        //webPrefab.dialogueIsActive = true;
        if (DragAndDrop.activeSpider != null) DragAndDrop.activeSpider.canMove = false;
        if (DragAndDrop.activeSpider != null) DragAndDrop.activeSpider.canShoot = false;
        // UNeble the possability to move Spider or Make a web while dialogue is active - we CAN NOT move
        Debug.Log("Freezed"); // true
    }

    public void UnfreezePlayer(int a)
    {
        StartCoroutine(UntilWebCanBeDeleted());
        if (DragAndDrop.activeSpider != null) DragAndDrop.activeSpider.canMove = true;
        if (DragAndDrop.activeSpider != null) DragAndDrop.activeSpider.canShoot = true;
        //webPrefab.dialogueIsActive = true;
        // ANeble the possability to move Spider or Make a web while dialogue isnt active - we CAN move now
        Debug.Log("UNfreezed"); // true
    }

    IEnumerator UntilWebCanBeDeleted()
    {
        yield return new WaitForEndOfFrame();
        webPrefab = FindAnyObjectByType<RopeVerlet>();
        if (webPrefab != null) webPrefab.dialogueIsActive = false;
    }    
}

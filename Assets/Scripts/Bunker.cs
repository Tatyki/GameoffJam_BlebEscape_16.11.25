using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Bunker : MonoBehaviour
{
    public BlebInBunker blebInBunker;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("bleb"))
        {
            blebInBunker.blebInBunkerCount++;
            SoundManager.PlaySound(SoundType.BlebSaved);
            ManagerOfScenes.TryLoadNextLevel();
            Destroy(collision.gameObject);
        }
    }
}

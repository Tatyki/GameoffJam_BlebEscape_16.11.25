using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterScript : MonoBehaviour
{
    public string knot;
    // public KeyCode interactButton;
    public bool smthActive;
    private DialogueManager DialogueManager;
    
    void Start()
    {
        DialogueManager = FindAnyObjectByType<DialogueManager>();

        //DialogueManager.StartDialogue(knot);
        StartCoroutine(DiaStarter());
    }

    private void Update()
    {
        if(smthActive)
        {
            DialogueManager.StartDialogue(knot);
        }
    }

    IEnumerator DiaStarter()
    {
        yield return new WaitForSeconds(1.8f);
        DialogueManager.StartDialogue(knot);
    }

}
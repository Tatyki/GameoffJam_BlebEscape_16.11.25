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

        DialogueManager.StartDialogue(knot);
    }

    private void Update()
    {
        if(smthActive)
        {
            DialogueManager.StartDialogue(knot);
        }
    }

}
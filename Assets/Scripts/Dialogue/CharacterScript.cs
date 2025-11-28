using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterScript : MonoBehaviour
{
    public Animator anim;
    public bool isTalking;
    public bool canTalk;
    public string knot;
    public KeyCode interactButton;
    private DialogueManager DialogueManager;
    
    void Start()
    {
        anim = GetComponent<Animator>();
        isTalking = false;
        DialogueManager = FindAnyObjectByType<DialogueManager>();
    }

    private void Update()
    {
        if(canTalk && Input.GetKeyDown(interactButton) && this.gameObject.name != "Player")
        {
            DialogueManager.StartDialogue(knot);
            canTalk = false;
        }
    }

    public void PlayAnimation(string _name)
    {
        switch (_name)
        {
            case "idle":
                anim.SetTrigger("toIdle");
                break;
            case "talk":
                isTalking = true;
                anim.SetTrigger("toTalk");
                break;
            case "think":
                anim.SetTrigger("toThink");
                break;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
       if(collision.tag == "Player")
        {
            canTalk = true;
        } 
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            canTalk = true;
        }
    }


}
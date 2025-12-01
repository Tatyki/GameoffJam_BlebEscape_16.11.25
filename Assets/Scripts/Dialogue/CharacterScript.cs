using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterScript : MonoBehaviour
{
    public string knot;
    // public KeyCode interactButton;
    private DialogueManager DialogueManager;

    string[] checkBool;
    public bool currentBool;
    public string[] knots;
    public int currentId;

    TutorialChecks tutorialChecks;
    
    void Start()
    {
        //checkBool = new bool[6];

        DialogueManager = FindAnyObjectByType<DialogueManager>();
        tutorialChecks = FindAnyObjectByType<TutorialChecks>();

        /*checkBool[0] = nameof(tutorialChecks.isSpiderActive());
        checkBool[1] = tutorialChecks.isSpiderMoved();
        checkBool[2] = tutorialChecks.isWebReleased();
        checkBool[3] = tutorialChecks.isBlebSaved();
        checkBool[4] = tutorialChecks.isWebCleared();
        checkBool[5] = tutorialChecks.isMyhaReachedLastPoint();*/




        //DialogueManager.StartDialogue(knot);
        StartCoroutine(DiaStarter());
    }

    private void Update()
    {
        switch (currentId)
        {
            case 0:
                currentBool = tutorialChecks.isSpiderActive();
                break;
            case 1:
                currentBool = tutorialChecks.isSpiderMoved();
                break;
            case 2:
                currentBool = tutorialChecks.isWebReleased();
                //currentBool = tutorialChecks.isWebCleared();
                break;
            case 3:
                currentBool = tutorialChecks.isBlebSaved();
                break;
            case 4:
                currentBool = tutorialChecks.isWebCleared();
                break;
            case 5:
                currentBool = tutorialChecks.isMyhaReachedLastPoint();
                break;
        }

        if (currentBool)
        {
            currentBool = false;
            DialogueManager.StartDialogue(knots[currentId]);
            currentId += 1;
        }
    }

    public void ChangeACheck(int a)
    {
        //currentId += a; 
    }



    IEnumerator DiaStarter()
    {
        yield return new WaitForSeconds(1.8f);
        DialogueManager.StartDialogue(knot);
    }

}
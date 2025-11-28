using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Selectable : MonoBehaviour
{
    public object element;

    public void Decide()
    {
        DialogueManager.SetDecision(element);
    }

    private void OnMouseDown()
    {
        Debug.Log("click");
        DialogueManager.SetDecision(element);
    }

}
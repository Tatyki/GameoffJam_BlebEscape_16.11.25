using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ink.Runtime;
using TMPro;
using System.Reflection;
using UnityEditor;

public class DialogueManager : MonoBehaviour
{
    [Header("Spesific")]
    public float textImputTime = 0.1f;
    [Header ("Main")]
    public TextAsset inkFile;
    public GameObject textBox;
    public GameObject customButton;
    public GameObject optionPanel;
    public bool isTalking = false;
    public bool dialogueHasStarted = false;
    public KeyCode nextButton;

    static Story story;
    TextMeshProUGUI nametag;
    TextMeshProUGUI message;
    List<string> tags;
    static Choice choiceSelected;

    // Start is called before the first frame update
    void Start()
    {
        story = new Story(inkFile.text);
        nametag = textBox.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        message = textBox.transform.GetChild(1).GetComponent<TextMeshProUGUI>();
        tags = new List<string>();
        choiceSelected = null;



        story.BindExternalFunction("Call",
        (string objectName, string scriptName, string methodName, string param) =>
        {
            GameObject obj = GameObject.Find(objectName);
            if (!obj) { Debug.LogError("Object not found: " + objectName); return; }

            var component = obj.GetComponent(scriptName);
            if (!component) { Debug.LogError("Component not found: " + scriptName); return; }

            var method = component.GetType().GetMethod(methodName);
            if (method == null) { Debug.LogError("Method not found: " + methodName); return; }

            object finalParam = param;
            if (int.TryParse(param, out var i)) finalParam = i;
            else if (float.TryParse(param, out var f)) finalParam = f;
            else if (bool.TryParse(param, out var b)) finalParam = b;

            method.Invoke(component, new object[] { finalParam });
        });
    }

    public void StartDialogue(string knotName)
    {
        try
        {
            story.ChoosePathString(knotName); //если кнота не находит ничего не стартует закомменчено для тестирования
            textBox.SetActive(true);
            AdvanceDialogue();
        }
        catch (System.Exception e)
        {
            Debug.Log($"Could not jump to path '{knotName}': {e.Message}");
            return;
        }
    }

    private void Update()
    {
        //Разговор
        if (Input.GetKeyDown(nextButton))
        {           
            //Is there more to the story?
            if (story.canContinue)
            {
                nametag.text = "";
                AdvanceDialogue();

                //Are there any choices?
                if (story.currentChoices.Count != 0)
                {
                    StartCoroutine(ShowChoices());
                    Debug.Log(story.currentChoices);
                }
            }
            else if(!optionPanel.activeInHierarchy || !story.canContinue) //если история закончилась и учесть, что могут отображаться опции, не скипать диалог 
            {
                FinishDialogue();
            }
        }
    }

    // Finished the Story (Dialogue)
    private void FinishDialogue()
    {
        Debug.Log("End of Dialogue!");
        dialogueHasStarted = false;
        textBox.SetActive(false);
    }

    // Advance through the story 
    void AdvanceDialogue()
    {
        string currentSentence = story.Continue();
        ParseTags();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(currentSentence));
    }

    // Type out the sentence letter by letter and make character idle if they were talking
    IEnumerator TypeSentence(string sentence)
    {
        message.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            message.text += letter;
            yield return new WaitForSeconds(textImputTime);
        }
        CharacterScript tempSpeaker = GameObject.FindObjectOfType<CharacterScript>();
        yield return null;
    }

    // Create then show the choices on the screen until one got selected
    IEnumerator ShowChoices()
    {
        Debug.Log("There are choices need to be made here!");
        List<Choice> _choices = story.currentChoices;
        textBox.SetActive(false);

        //for (int i = 0; i < _choices.Count; i++)
        //{         
        //    GameObject temp = Instantiate(customButton, optionPanel.transform);
        //    temp.transform.GetChild(0).GetComponent<TextMeshPro>().text = _choices[i].text;
        //    temp.AddComponent<Selectable>();
        //    temp.GetComponent<Selectable>().element = _choices[i];
        //    //temp.GetComponent<Button>().onClick.AddListener(() => { temp.GetComponent<Selectable>().Decide(); });
        //}

        int count = _choices.Count;
        float radius = 3f;
        Vector3 center = optionPanel.transform.position; // или любой другой центр
        float startAngle = 0f;
        float endAngle = 180f;

        if (count == 1)
        {
            float midAngle = (startAngle + endAngle) * 0.5f;
            float rad = midAngle * Mathf.Deg2Rad;
            Vector3 pos = center + new Vector3(Mathf.Cos(rad), Mathf.Sin(rad), 0f) * radius;
            Instantiate(customButton, pos, Quaternion.identity, optionPanel.transform);
        }
        else
        {
            float step = (endAngle - startAngle) / (count - 1);
            for (int i = 0; i < count; i++)
            {
                float angleDeg = startAngle + step * i;
                float rad = angleDeg * Mathf.Deg2Rad;
                Vector3 pos = center + new Vector3(Mathf.Cos(rad), Mathf.Sin(rad), 0f) * radius;
                GameObject temp = Instantiate(customButton, pos, Quaternion.identity, optionPanel.transform);
                temp.transform.GetChild(0).GetComponent<TextMeshPro>().text = _choices[i].text;
                temp.AddComponent<Selectable>();
                temp.GetComponent<Selectable>().element = _choices[i];
                // опционально повернуть так, чтобы "внешняя" сторона смотрела наружу:
                // temp.transform.rotation = Quaternion.Euler(0f, 0f, angleDeg - 90f);
            }
        }

        optionPanel.SetActive(true);

        yield return new WaitUntil(() => { return choiceSelected != null; });

        textBox.SetActive(true);
        AdvanceFromDecision();
    }   

    // Tells the story which branch to go to
    public static void SetDecision(object element)
    {
        choiceSelected = (Choice)element;
        story.ChooseChoiceIndex(choiceSelected.index);
    }

    // After a choice was made, turn off the panel and advance from that choice
    void AdvanceFromDecision()
    {
        optionPanel.SetActive(false);
        for (int i = 0; i < optionPanel.transform.childCount; i++)
        {
            Destroy(optionPanel.transform.GetChild(i).gameObject);
        }
        choiceSelected = null; // Forgot to reset the choiceSelected. Otherwise, it would select an option without player intervention.
        AdvanceDialogue();
    }

    /*** Tag Parser ***/
    /// In Inky, you can use tags which can be used to cue stuff in a game.
    /// This is just one way of doing it. Not the only method on how to trigger events. 
    void ParseTags()
    {
        tags = story.currentTags;
        foreach (string t in tags)
        {
            string prefix = t.Split(' ')[0];
            string param = t.Split(' ')[1];

            switch (prefix.ToLower())
            {
                case "anim":
                    SetAnimation(param);
                    break;
                case "color":
                    SetTextColor(param);
                    break;
                case "talker":
                    SetDialogueOnTalkingCharacter(param);
                    break;
                case "name":
                    SetName(param);
                    break;
                case "function":
                    CallFunction(param);
                    break;
            }
        }
    }
    void SetAnimation(string _name)
    {
        string objName = _name.Split('_')[0];
        string animName = _name.Split("_")[1];

        GameObject obj = GameObject.Find(objName);
        obj.GetComponent<Animator>().Play(animName);
    }
    void SetTextColor(string _color)
    {
        switch (_color)
        {
            case "red":
                message.color = Color.red;
                break;
            case "blue":
                message.color = Color.cyan;
                break;
            case "green":
                message.color = Color.green;
                break;
            case "white":
                message.color = Color.white;
                break;
            default:
                Debug.Log($"{_color} is not available as a text color");
                break;
        }
    }
    void SetDialogueOnTalkingCharacter(string name)
    {
        GameObject talkingCharacter = GameObject.Find(name);
        textBox.transform.position = talkingCharacter.transform.GetChild(0).position;
    }

    void SetName(string name)
    {
        nametag.text = name;
    }

    void CallFunction(string param)
    {
        string objName = param.Split('_')[0];
        string nameOfScript = param.Split("_")[1];
        string functionName = param.Split("_")[2];
        string parameter = param.Split("_")[3];

        GameObject obj = GameObject.Find(objName);
        obj.GetComponent(nameOfScript).SendMessage(functionName, parameter, SendMessageOptions.DontRequireReceiver);
    }

    }
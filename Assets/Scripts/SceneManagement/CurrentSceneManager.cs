using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrentSceneManager : MonoBehaviour
{
    public BlebData blebData;
    public BlebInBunker blebInBunker;
    [Space]
    public string nextLevel;
    public string loseScene;
    public string mainMenu;

    private void Awake()
    {
        ManagerOfScenes.NextLevel = nextLevel;
        ManagerOfScenes.LoseScene = loseScene;
        ManagerOfScenes.MainMenu = mainMenu;

        ManagerOfScenes.Init(blebData, blebInBunker);
    }
}

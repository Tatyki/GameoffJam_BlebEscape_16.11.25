using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class ManagerOfScenes
{

    public static string LoseScene;
    public static string NextLevel;
    public static string MainMenu;


    private static BlebData blebData;
    private static BlebInBunker blebInBunker;


    public static void Init(BlebData data, BlebInBunker bunker)
    {
        blebData = data;
        blebInBunker = bunker;
    }

    public static void TryLoadNextLevel()
    {
        if (blebInBunker.blebInBunkerCount == blebData.blebCount)
        {
            //this will activate when lived bleb are saved
            SceneManager.LoadScene(NextLevel);
        }
    }

    public static void TryLoadMainMenu()
    {
        SceneManager.LoadScene(MainMenu);
    }

    public static void TryLoadLoseScene()
    {
        if (blebData.blebCount == 0)
        {
            //this will activate when all bleb is dead :(
            SceneManager.LoadScene(LoseScene);
        }
    }


    public static void Load(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}

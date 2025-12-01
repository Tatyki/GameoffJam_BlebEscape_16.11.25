using UnityEngine;
using UnityEngine.SceneManagement;

public class DiaSceneChanger : MonoBehaviour
{
    public string educationLevel;
    public string secondLevel;
    public void StartEducationComic(int a)
    {
        SceneManager.LoadScene(educationLevel, LoadSceneMode.Single);
    }
    public void StartSecondLevel(int a)
    {
        SceneManager.LoadScene(secondLevel, LoadSceneMode.Single);
    }
}

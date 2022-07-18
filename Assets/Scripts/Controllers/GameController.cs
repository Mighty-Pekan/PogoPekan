using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoSingleton<GameController>
{

    private void Start()
    {
        Screen.orientation = ScreenOrientation.LandscapeLeft; //or right for right landscape
    }

    public static void GameOver() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public static void LoadNextLevel() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
    }

    public static int GetLevelsCount()
    {
        return SceneManager.sceneCountInBuildSettings;
    }

    public static void LoadLevel(int buildIndex)
    {
        SceneManager.LoadScene(buildIndex);
    }

}

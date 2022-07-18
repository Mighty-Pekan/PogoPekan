using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoSingleton<GameController>
{
    public static void GameOver() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public static void LoadNextLevel() {
        Debug.Log("Current Scene: " + SceneManager.GetActiveScene().buildIndex);
        Debug.Log("Next Scene: " + SceneManager.GetActiveScene().buildIndex + 1);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
    }


}

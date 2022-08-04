using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoSingleton<GameController>
{
    Player player;
    InputManager inputManager;
    public bool IsPause;

    private void Start()
    {
        Screen.orientation = ScreenOrientation.LandscapeLeft; //or right for right landscape
    }

    private void Update()
    {
        if(IsPause)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }
    }

    public static void GameOver() {
        SceneManager.LoadScene(0);
    }

    public static void LoadNextLevel() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
    }
    public static Player GetPlayer()
    {
        return instance.player;
    }
    //==============================================================registering objects
    public static void RegisterInputManager(InputManager _inputManager)
    {
        instance.inputManager = _inputManager;
    }
    public static void RegisterPlayer(Player _player) {
        instance.player = _player;
    }
    //============================================================== getters

    public static InputManager GetInputManager() {
        return instance.inputManager;
    }
    public static int GetLevelsCount() {
        return SceneManager.sceneCountInBuildSettings;
    }
    //==============================================================
    public static void ResetPlayerPosition()
    {
        instance.player.ResetInitialPosition();
    }


    public static void LoadLevel(string levelName)
    {
        SceneManager.LoadScene(levelName);
    }

}

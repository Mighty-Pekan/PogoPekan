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

    public void GameOver() {
        SceneManager.LoadScene(1);
    }

    public void LoadNextLevel() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
    }
    public Player GetPlayer()
    {
        return player;
    }
    //==============================================================registering objects
    public void RegisterInputManager(InputManager _inputManager)
    {
        inputManager = _inputManager;
    }
    public void RegisterPlayer(Player _player) {
        player = _player;
    }
    //============================================================== getters

    public InputManager GetInputManager() {
        return inputManager;
    }
    public int GetLevelsCount() {
        return SceneManager.sceneCountInBuildSettings;
    }
    //==============================================================
    public void ResetPlayerPosition()
    {
        player.ResetInitialPosition();
    }


    public void LoadLevel(string levelName)
    {
        SceneManager.LoadScene(levelName);
    }

}

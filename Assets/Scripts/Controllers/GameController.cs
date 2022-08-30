using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;
using System;

public class GameController : MonoSingleton<GameController> {
    public bool IsPause;

    private Player player;
    private InputManager inputManager;
    private FadePanel fadePanel;
    private Timer timer;

    private void Start() {
        Screen.orientation = ScreenOrientation.LandscapeLeft; //or right for right landscape
    }

    private void Update() {
        if (IsPause) {
            Time.timeScale = 0;
        }
        else {
            Time.timeScale = 1;
        }
        Debug.Log("current world: " + getCurrentWorld());
        Debug.Log("current level: " + GetCurrentLevel());
    }

    public void GameOver() {
        SceneManager.LoadScene(1);
    }

    public Player GetPlayer() {
        return player;
    }
    //==============================================================registering objects
    public void RegisterInputManager(InputManager _inputManager) {
        inputManager = _inputManager;
    }
    public void RegisterPlayer(Player _player) {
        player = _player;
    }
    public void RegisterFadePanel(FadePanel _fadePanel) {
        fadePanel = _fadePanel;
    }
    public void RegisterTimer(Timer _timer) {
        timer = _timer;
    }
    //============================================================== getters

    public InputManager GetInputManager() {
        return inputManager;
    }

    //==============================================================
    public void ResetPlayerPosition() {
        player.ResetInitialPosition();
    }

    //=============================================================== CUSTOM SCENE MANAGER
    public void LoadNextLevel() {
        LoadLevel(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void LoadLevel(string levelName) {StartCoroutine(LoadLevelCor(levelName));}
    public void LoadLevel(int levelNum) { StartCoroutine(LoadLevelCor(levelNum)); }

    private IEnumerator LoadLevelCor(string levelName) {
        yield return StartCoroutine(fadePanel.Apear());
        if(timer!=null)timer.Reset();
        SceneManager.LoadScene(levelName);
        if (UIManager.Instance != null) UIManager.Instance.OpenPausePanel(false);
        AudioManager.Instance.ChangeMusic();
    }
    private IEnumerator LoadLevelCor(int levelNum) {
        yield return StartCoroutine(fadePanel.Apear());
        if(timer!=null)timer.Reset();
        SceneManager.LoadScene(levelNum);
        if(UIManager.Instance !=null)UIManager.Instance.OpenPausePanel(false);
        AudioManager.Instance.ChangeMusic();
    }

    public int GetLevelsCount() {
        return SceneManager.sceneCountInBuildSettings;
    }

    public bool isStartMenu() {
        return SceneManager.GetActiveScene().buildIndex == 0;
    }

    public int getCurrentWorld() {
        if (isStartMenu()) return -1;
        return (int)Char.GetNumericValue(SceneManager.GetActiveScene().name.ToCharArray()[0]);
    }

    public int GetCurrentLevel() {
        if (isStartMenu()) return -1;
        return (int)Char.GetNumericValue(SceneManager.GetActiveScene().name.ToCharArray()[2]);
    }


}

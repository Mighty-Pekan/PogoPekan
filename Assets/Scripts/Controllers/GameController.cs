using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoSingleton<GameController> {
    Player player;
    InputManager inputManager;
    public bool IsPause;
    FadePanel fadePanel;

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
    public void LoadLevel(string levelName) { StartCoroutine(LoadLevelCor(levelName)); }
    public void LoadLevel(int levelNum) { StartCoroutine(LoadLevelCor(levelNum)); }



    private IEnumerator LoadLevelCor(string levelName) {
        yield return StartCoroutine(fadePanel.Apear());
        SceneManager.LoadScene(levelName);
        if (UIManager.Instance != null) UIManager.Instance.OpenPausePanel(false);
    }
    private IEnumerator LoadLevelCor(int levelNum) {
        yield return StartCoroutine(fadePanel.Apear());
        SceneManager.LoadScene(levelNum);
        if(UIManager.Instance !=null)UIManager.Instance.OpenPausePanel(false);
    }

    public int GetLevelsCount() {
        return SceneManager.sceneCountInBuildSettings;
    }

    private IEnumerator cor() {
        yield return null;
    }

    public bool isStartMenu() {
        return SceneManager.GetActiveScene().buildIndex == 0;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class GameController : MonoSingleton<GameController> {
    public bool IsPause;

    private Player player;
    private InputManager inputManager;
    private FadePanel fadePanel;
    private Timer timer;

    public int NumLevelsPerWorld { get => numLevelsPerWorld; }
    public int NumWorlds { get => numWorlds; }

    [Header("Settings")]
    [SerializeField] int numLevelsPerWorld;
    [SerializeField] int numWorlds;


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
        int[] nextLevel = GetNextLevel();
        LevelsDataManager.Instance.UnlockLevel(nextLevel[0], nextLevel[1]);
        LoadLevel(nextLevel[0], nextLevel[1]);
    }

     
    public void ReturnToMainMenu() {
        SceneManager.LoadScene("Menu");

        UIManager.Instance.OpenPausePanel(false);
        AudioManager.Instance.ChangeMusic();

    }
    public void LoadLevel(string levelName) {StartCoroutine(LoadLevelCor(levelName));}
    public void LoadLevel(int world, int level) { StartCoroutine(LoadLevelCor(world, level));}

    private IEnumerator LoadLevelCor(string levelName) {
        yield return StartCoroutine(fadePanel.Apear());
        if(timer!=null)timer.Reset();
        SceneManager.LoadScene(levelName);
        if (UIManager.Instance != null) UIManager.Instance.OpenPausePanel(false);
        AudioManager.Instance.ChangeMusic();
    }
    private IEnumerator LoadLevelCor(int world,int level) {
        yield return StartCoroutine(fadePanel.Apear());
        if(timer!=null)timer.Reset();
        SceneManager.LoadScene(world.ToString()+"."+level.ToString());
        if(UIManager.Instance !=null)UIManager.Instance.OpenPausePanel(false);
        AudioManager.Instance.ChangeMusic();
    }

    public int GetLevelsCount() {
        return SceneManager.sceneCountInBuildSettings;
    }

    public bool isStartMenu() {
        return SceneManager.GetActiveScene().buildIndex == 0;
    }

    public int GetCurrentWorld() {
        if (isStartMenu()) return -1;
        return (int)Char.GetNumericValue(SceneManager.GetActiveScene().name.ToCharArray()[0]);
    }

    public int GetCurrentLevel() {
        if (isStartMenu()) return -1;
        return (int)Char.GetNumericValue(SceneManager.GetActiveScene().name.ToCharArray()[2]);
    }

    public int[] GetNextLevel() {

        int[] ris = new int[2]; 
        
        int currentLevel = GetCurrentLevel();
        int currentWorld = GetCurrentWorld();

        if(currentLevel == NumLevelsPerWorld) {
            ris[0] = currentWorld + 1;
            ris[1] = 1;
        }
        else {
            ris[0] = currentWorld;
            ris[1] = currentLevel+1;
        }
        return ris;
    }


}

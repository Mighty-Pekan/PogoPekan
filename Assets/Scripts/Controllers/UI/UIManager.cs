using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class UIManager : MonoSingleton<UIManager>
{
    [SerializeField] GameObject PausePanel;
    [SerializeField] GameObject PauseButton;
    [SerializeField] TextMeshProUGUI FunnyText;
    [SerializeField] GameObject LevelCompletedPanel;
    [SerializeField] GameObject TimerPanel;
    [SerializeField] TextMeshProUGUI LevelCompletedTimeText;
    [SerializeField] Image [] FishIndicators;
    [SerializeField] GenericAudioSource buttonClickAudioSource;

    [SerializeField] List<string> funnyTexts = new List<string>();

    private void Start()
    {
        if(PausePanel != null)
            PausePanel.SetActive(false);

        if(PauseButton != null)
            PauseButton.SetActive(true);

        if (LevelCompletedPanel != null)
            LevelCompletedPanel.SetActive(false);
    }

    public void OpenPausePanel(bool isOpen)
    {
        if (GameController.Instance.canPauseBeCalled) {
            if (PausePanel != null)
                PausePanel.SetActive(isOpen);
            if (PausePanel != null)
                PauseButton.SetActive(!isOpen);

            GameController.Instance.IsPause = isOpen;

            FunnyText.text = GetRandomText();
        }
    }

    public void ReturnToMainMenu()
    {
        GameController.Instance.ReturnToMainMenu();
    }

    private string GetRandomText()
    {
        return funnyTexts[Random.Range(0, funnyTexts.Count)];
    }

    public void ShowLevelCompletedPanel(int levelTime) {
        LevelCompletedPanel.SetActive(true );
        TimerPanel.SetActive(false);
        PauseButton.gameObject.SetActive(false);
        LevelCompletedTimeText.text = "Time: " +levelTime.ToString();

        int[] level = GameController.Instance.GetCurrentLevel();
        for (int i = 0; i < LevelsDataManager.Instance.GetNumFishFound(level[0], level[1]); i++) FishIndicators[i].color = Color.white;
    }
    public void HideLevelCompletedPanel() {
        LevelCompletedPanel.SetActive(false);
        TimerPanel.SetActive(true);
        PauseButton.gameObject.SetActive(true);
    }

    public void MakeButtonSound() {
        buttonClickAudioSource.Play();
    }

}

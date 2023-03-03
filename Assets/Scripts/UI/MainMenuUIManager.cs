using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuUIManager : MonoBehaviour
{
    [SerializeField] GameObject MainMenuPanel;
    [SerializeField] GameObject LevelsSelectionPanel;
    [SerializeField] AudioSource buttonClickAudioSource;

    private void Start()
    {
        MainMenuPanel.SetActive(!GameController.Instance.ShowLevelsPanel);
        LevelsSelectionPanel.SetActive(GameController.Instance.ShowLevelsPanel);
    }

    public void MakeButtonSound()
    {
        buttonClickAudioSource.Play();
    }

    public void QuitGame()
    {
        Application.Quit();
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoSingleton<UIManager>
{
    [SerializeField] GameObject PausePanel;
    [SerializeField] GameObject PauseButton;

    private void Start()
    {
        PausePanel.SetActive(false);
        PauseButton.SetActive(true);
    }

    public void OpenPausePanel(bool isOpen)
    {
        PausePanel.SetActive(isOpen);
        PauseButton.SetActive(!isOpen);
        GameController.Instance.IsPause = isOpen;
    }

    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}

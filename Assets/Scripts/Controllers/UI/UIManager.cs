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
        if(PausePanel != null)
            PausePanel.SetActive(false);

        if(PauseButton != null)
        PauseButton.SetActive(true);
    }

    public void OpenPausePanel(bool isOpen)
    {
        if (PausePanel != null)
            PausePanel.SetActive(isOpen);
        if (PausePanel != null)
            PauseButton.SetActive(!isOpen);
        GameController.Instance.IsPause = isOpen;
    }

    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}

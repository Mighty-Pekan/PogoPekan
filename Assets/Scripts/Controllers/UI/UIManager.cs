using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
public class UIManager : MonoSingleton<UIManager>
{
    [SerializeField] GameObject PausePanel;
    [SerializeField] GameObject PauseButton;
    [SerializeField] TextMeshProUGUI FunnyText;

    [SerializeField] List<string> funnyTexts = new List<string>();

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

        FunnyText.text = GetRandomText();

    }

    public void ReturnToMainMenu()
    {
        GameController.Instance.ReturnToMainMenu();
    }

    private string GetRandomText()
    {
        return funnyTexts[Random.Range(0, funnyTexts.Count)];
    }
}

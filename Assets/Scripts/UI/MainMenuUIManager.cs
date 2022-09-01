using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuUIManager : MonoBehaviour
{
    [SerializeField] GameObject MainMenuPanel;
    [SerializeField] GameObject LevelsSelectionPanel;

    private void Start() {
        MainMenuPanel.SetActive(!GameController.Instance.ShowLevelsPanel);
        LevelsSelectionPanel.SetActive(GameController.Instance.ShowLevelsPanel);
    }
}

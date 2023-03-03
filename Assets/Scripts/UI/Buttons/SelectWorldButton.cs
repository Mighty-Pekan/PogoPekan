using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SelectWorldButton : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] int world = 1;
    [SerializeField] GameObject panelToActivate;

    [SerializeField] GameObject mainBackground;
    [SerializeField] GameObject backgroundW1;
    [SerializeField] GameObject backgroundW2;
    [SerializeField] GameObject backgroundW3;
    [SerializeField] GameObject backgroundW4;


    private Button myButton;

    private void Start() {
        myButton = GetComponent<Button>();
        myButton.GetComponentInChildren<TextMeshProUGUI>().text = "World " + world;

        if (LevelsDataManager.Instance.IsLevelUnlocked(world, 1)) {
            myButton.interactable = true;
            myButton.image.color = Color.white;
            myButton.GetComponentInChildren<TextMeshProUGUI>().color = Color.white;
            GetComponent<ButtonUIHover>().IsInteractable = true;
        }
        else {
            myButton.image.color = GameController.Instance.GetLockedColor();
            myButton.GetComponentInChildren<TextMeshProUGUI>().color = GameController.Instance.GetLockedTextColor();
            GetComponent<ButtonUIHover>().IsInteractable = false;
        }
    }

    public void SelectWorld() {
        GameController.Instance.SelectedWorld = world;
        panelToActivate.SetActive(true);
        switch (world)
        {
            case 1: backgroundW1.SetActive(true);
                    backgroundW2.SetActive(false);
                    backgroundW3.SetActive(false);
                    backgroundW4.SetActive(false);
                    mainBackground.SetActive(false);
                    break;
            case 2:
                    backgroundW1.SetActive(false);
                    backgroundW2.SetActive(true);
                    backgroundW3.SetActive(false);
                    backgroundW4.SetActive(false);
                    mainBackground.SetActive(false);
                    break;

            case 3:
                    backgroundW1.SetActive(false);
                    backgroundW2.SetActive(false);
                    backgroundW3.SetActive(true);
                    backgroundW4.SetActive(false);
                    mainBackground.SetActive(false);
                    break;

            case 4:
                    backgroundW1.SetActive(false);
                    backgroundW2.SetActive(false);
                    backgroundW3.SetActive(false);
                    backgroundW4.SetActive(true);
                    mainBackground.SetActive(false);
                    break;
        }
    }

    //private void IEN
}

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

    private Button myButton;

    private void Start() {
        myButton = GetComponent<Button>();
        myButton.GetComponentInChildren<TextMeshProUGUI>().text = "World " + world;

        if (LevelsDataManager.Instance.IsLevelUnlocked(world, 1)) {
            myButton.interactable = true;
            myButton.image.color = Color.white;
            myButton.GetComponentInChildren<TextMeshProUGUI>().color = Color.white;
            GetComponent<Hover>().CanExpand = true;
        }
        else {
            myButton.image.color = GameController.Instance.GetLockedColor();
            myButton.GetComponentInChildren<TextMeshProUGUI>().color = GameController.Instance.GetLockedTextColor();
            GetComponent<Hover>().CanExpand = false;
        }
    }

    public void SelectWorld() {
        GameController.Instance.SelectedWorld = world;
        panelToActivate.SetActive(true);
    }

    //private void IEN
}

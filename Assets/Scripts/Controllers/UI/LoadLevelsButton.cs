using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class LoadLevelsButton : MonoBehaviour
{
    private Button myButton;

    [Header("Settings")]
    [SerializeField] int world = 1;
    [SerializeField] int level = 1;

    private void Start() {

        myButton = GetComponent<Button>();
        myButton.GetComponentInChildren<TextMeshProUGUI>().text = "Level " + level;;

        if (LevelsDataManager.Instance.isLevelUnlocked(world,level)) {
            myButton.interactable = true;
            myButton.image.color = Color.white;
            myButton.GetComponentInChildren<TextMeshProUGUI>().color = Color.white;
        }
    }

    public void LoadLevel() {
        GameController.Instance.LoadLevel(world + "."+level);
    }

}

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
    [SerializeField] int level = 1;
    [SerializeField] Image[] fishIndicators;
    [SerializeField] Image bestTimePanel;
    [SerializeField] TextMeshProUGUI bestTimeText;

    private void Awake() {
        myButton = GetComponent<Button>();
        myButton.GetComponentInChildren<TextMeshProUGUI>().text = "Level " + level;
    }

    private void OnEnable() {
        //if(level ==1)Debug.Log("enable called with world: "+ GameController.Instance.SelectedWorld);

        if (LevelsDataManager.Instance.IsLevelUnlocked(GameController.Instance.SelectedWorld, level)) {
            Debug.Log("level "+GameController.Instance.SelectedWorld+"."+level+" is UNLOCKED");
            myButton.interactable = true;
            myButton.image.color = Color.white;
            myButton.GetComponentInChildren<TextMeshProUGUI>().color = Color.white;
            GetComponent<Hover>().CanExpand = true;
            evaluateFishesIndicatorsColor();
            int bestTime = LevelsDataManager.Instance.GetBestTime(GameController.Instance.SelectedWorld, level);
            if (bestTime == -1) bestTimeText.text = "----";
            else {
                bestTimeText.text = bestTime.ToString();
            }
            bestTimeText.color = Color.white;
            bestTimePanel.color = Color.white;
        }
        else {
            Debug.Log("level " + GameController.Instance.SelectedWorld + "." + level + " is locked");
            myButton.interactable = false;
            myButton.image.color = GameController.Instance.GetLockedColor();
            myButton.GetComponentInChildren<TextMeshProUGUI>().color = GameController.Instance.GetLockedTextColor();
            evaluateFishesIndicatorsColor();
            GetComponent<Hover>().CanExpand = false;
        }
    }

    private void evaluateFishesIndicatorsColor() {
        for (int i = 0; i < fishIndicators.Length; i++) {
            if (i < LevelsDataManager.Instance.GetNumFishFound(GameController.Instance.SelectedWorld, level))
                fishIndicators[i].color = Color.white;
            else fishIndicators[i].color = Color.black;
        }
    }

    public void LoadLevel() {
        if (GameController.Instance.CanLevelButtonsBePressed){
            GameController.Instance.CanLevelButtonsBePressed = false;
            GameController.Instance.LoadLevel(GameController.Instance.SelectedWorld + "." + level);
        } 
    }

}

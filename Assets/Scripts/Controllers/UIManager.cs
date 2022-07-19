using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoSingleton<UIManager>
{
    public GameObject debuggerPanel;
    bool debuggerIsActive = true;

    public GameObject selectLevelPanel;
    bool selectLevelPanelIsActive = false;

    public GameObject selectLevelButtonPrefab;

    Button selectLevelButton;

    public TextMeshProUGUI fpsText;
    float deltaTime;


    private void Start()
    {
        DisplayAllLevels();
    }

    private void Update()
    {
        debuggerPanel.SetActive(debuggerIsActive);
        selectLevelPanel.SetActive(selectLevelPanelIsActive);
        ShowFPS();
    }

    public void EnableDebuggerPanel()
    {
        debuggerIsActive = !debuggerIsActive;
    }

    public void EnableSelectLevelPanel()
    {
        selectLevelPanelIsActive = !selectLevelPanelIsActive;
    }

    void ShowFPS()
    {
        deltaTime += (Time.deltaTime - deltaTime) * 0.1f;
        float fps = 1.0f / deltaTime;
        fpsText.text = "FPS " + Mathf.Ceil(fps) ;
    }

    void DisplayAllLevels()
    {
        for (int i = 1; i < GameController.GetLevelsCount(); i++)
        {
            Debug.Log("Index " + i);
            GameObject buttonInstance = Instantiate(selectLevelButtonPrefab, selectLevelPanel.transform.position, Quaternion.identity);
            buttonInstance.name = "Level" + i;
            buttonInstance.transform.parent = selectLevelPanel.transform;
            buttonInstance.transform.GetChild(0).gameObject.GetComponent<Text>().text = buttonInstance.name;

            selectLevelButton = buttonInstance.GetComponent<Button>();

            selectLevelButton.onClick.AddListener(() => GameController.LoadLevel(buttonInstance.name));
          
        }
    }

    public void ResetPlayerPosition() {
        GameController.ResetPlayerPosition();
    }

    public void EnableTouchInput() {
        GameController.EnableTouchInput();
    }


}

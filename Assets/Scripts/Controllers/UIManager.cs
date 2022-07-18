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
        for (int i = 0; i < GameController.GetLevelsCount(); i++)
        {
            Debug.Log("Index " + i);
            GameObject buttonInstance = Instantiate(selectLevelButtonPrefab, selectLevelPanel.transform.position, Quaternion.identity);
            buttonInstance.name = "Level " + i;
            buttonInstance.transform.parent = selectLevelPanel.transform;

            buttonInstance.GetComponent<Button>().onClick.AddListener(delegate { GameController.LoadLevel(1); });
            /*buttonInstance.GetComponent<Button>().onClick.AddListener(() => { GameController.LoadLevel(i); });*/
        }

    }
}

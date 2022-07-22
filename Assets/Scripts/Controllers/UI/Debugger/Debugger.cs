using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Debugger : MonoSingleton<Debugger>
{
    [SerializeField] GameObject MainPanel;
    [SerializeField] GameObject selectLevelPanel;

    [SerializeField] GameObject selectLevelButtonPrefab;
    Button selectLevelButton;

    [SerializeField] TextMeshProUGUI fpsText;
    float deltaTime;

    private void Start()
    {
        DisplayAllLevels();
    }

    void DisplayAllLevels() {
        for (int i = 1; i < GameController.GetLevelsCount(); i++) {
            Debug.Log("Index " + i);
            GameObject buttonInstance = Instantiate(selectLevelButtonPrefab, selectLevelPanel.transform.position, Quaternion.identity);
            buttonInstance.name = "Level" + i;
            buttonInstance.transform.parent = selectLevelPanel.transform;
            buttonInstance.transform.GetChild(0).gameObject.GetComponent<Text>().text = buttonInstance.name;

            selectLevelButton = buttonInstance.GetComponent<Button>();

            selectLevelButton.onClick.AddListener(() => GameController.LoadLevel(buttonInstance.name));

        }
    }

    private void Update()
    {
        ShowFPS();
    }

    public void toggleShowMainPanel()
    {
        if(MainPanel.activeSelf)
            MainPanel.SetActive(false);
        else 
            MainPanel.SetActive(true);
    }

    public void toggleShowSelectLevelPanel()
    {
        if(selectLevelPanel.activeSelf)
            selectLevelPanel.SetActive(false);
        else
            selectLevelPanel.SetActive(true);
    }

    void ShowFPS()
    {
        deltaTime += (Time.deltaTime - deltaTime) * 0.1f;
        float fps = 1.0f / deltaTime;
        fpsText.text = "FPS " + Mathf.Ceil(fps) ;
    }

    

    /// //////////////////

    public void toggleInputType() {
        GameController.GetInputManager().ToggleTouchInput();
    }
    public void resetPlayerPosition() {
        GameController.ResetPlayerPosition();
    }



}

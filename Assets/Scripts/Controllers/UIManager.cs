using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    public GameObject debuggerPanel;
    bool debuggerIsActive = true;

    public TextMeshProUGUI fpsText;
    float deltaTime;

    private void Update()
    {
        debuggerPanel.SetActive(debuggerIsActive);
        ShowFPS();
    }

    public void EnableDebuggerPanel()
    {
        debuggerIsActive = !debuggerIsActive;
    }

    void ShowFPS()
    {
        deltaTime += (Time.deltaTime - deltaTime) * 0.1f;
        float fps = 1.0f / deltaTime;
        fpsText.text = "FPS " + Mathf.Ceil(fps) ;
    }
}

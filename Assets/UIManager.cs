using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public GameObject debuggerPanel;
    bool debuggerIsActive = true;

    private void Update()
    {
        debuggerPanel.SetActive(debuggerIsActive);
    }

    public void EnableDebuggerPanel()
    {
        debuggerIsActive = !debuggerIsActive;
    }

   
}

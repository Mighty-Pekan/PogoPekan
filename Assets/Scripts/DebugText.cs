using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DebugText : MonoSingleton<DebugText>
{
    private void Start() {
        GetComponent<Text>().text = "";
    }
    public void Log(string newText) {
        GetComponent<Text>().text += newText+'\n';
    }
}

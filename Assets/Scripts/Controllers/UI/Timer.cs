using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    private float startTime;
    private TextMeshProUGUI timerText;

    private void Start() {
        startTime = Time.time;
        timerText = GetComponent<TextMeshProUGUI>();
    }

    public void Update() {
        int newTime = (int)(Time.time - startTime);
        if (newTime < 0) newTime = 0;
        else if (newTime > 9999) newTime = 9999;

        timerText.text = newTime.ToString();
    }

    public void Reset() {
        startTime = Time.time;
    }

}

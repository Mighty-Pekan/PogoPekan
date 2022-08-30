using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI maxTimeForFish;
    private float startTime;
    private TextMeshProUGUI timerText;
    private int actualTime;

    private void Awake() {
        GameController.Instance.RegisterTimer(this);
    }

    private void Start() {
        startTime = Time.time;
        timerText = GetComponent<TextMeshProUGUI>();
        maxTimeForFish.text = GameController.Instance.GetMaxTimeForFish().ToString();
    }

    public void Update() {
        actualTime = (int)(Time.time - startTime);
        if (actualTime < 0) actualTime = 0;
        else if (actualTime > 9999) actualTime = 9999;

        timerText.text = actualTime.ToString();
    }

    public void Reset() {
        startTime = Time.time;
    }

    public int GetTime() { return actualTime; }

}

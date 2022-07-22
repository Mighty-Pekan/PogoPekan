using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExitQuestionario : MonoSingleton<ExitQuestionario>
{
    [SerializeField] GameObject QuestionarioPanel;

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.tag == "Player") {
            Debug.Log("trigger");
            QuestionarioPanel.SetActive(true);
            other.gameObject.SetActive(false);
        }   
    }
}

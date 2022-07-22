using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExitQuestionario : MonoSingleton<ExitQuestionario>
{
    [SerializeField] GameObject QuestionarioPanel;

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.tag == "Player") {
            Application.OpenURL("https://docs.google.com/forms/d/1Jdntp2Cx8-1V-_RIvJqM5gd-JBbO5aWKb7KaBevvvgw/prefill");
            //Debug.Log("trigger");
            //QuestionarioPanel.SetActive(true);
            //other.gameObject.SetActive(false);
        }   
    }
}

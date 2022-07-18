using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Exit : MonoBehaviour
{
    [SerializeField] GameObject ButtonQuestionario;

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.tag == "Player") {
            Debug.Log("trigger");
            ButtonQuestionario.SetActive(true);
            other.gameObject.SetActive(false);
        }   
    }
}

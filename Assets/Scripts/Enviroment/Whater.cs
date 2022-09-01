using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Whater : MonoBehaviour
{
    [SerializeField] private float timeToKill = 0.5f;
    private float? enterTime = null;

    private void OnTriggerEnter2D(Collider2D collision) {
        enterTime = Time.time;
    }
    private void OnTriggerExit2D(Collider2D collision) {
        enterTime = null;
    }
    private void OnTriggerStay2D(Collider2D collision) {
        if(collision.gameObject.tag == "Player" && (Time.time - enterTime > timeToKill)) {
            GameController.Instance.GameOver();
        }
    }
}

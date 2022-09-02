using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Whater : MonoBehaviour
{
    [SerializeField] private float timeToKill = 0.5f;
    [SerializeField] private GameObject WhaterParticle;
    [SerializeField] private AudioClip WhaterSound;
    private float? enterTime = null;

    private void OnTriggerEnter2D(Collider2D collision) {
        if(collision.gameObject.tag == "Player") {
            enterTime = Time.time;
            Instantiate(WhaterParticle, collision.gameObject.transform.position, Quaternion.identity);
            AudioManager.Instance.PlaySound(WhaterSound);
        }
    }
    private void OnTriggerExit2D(Collider2D collision) {
        if (collision.gameObject.tag == "Player") {
            enterTime = null;
        }
    }
    private void OnTriggerStay2D(Collider2D collision) {
        if(collision.gameObject.tag == "Player" && (Time.time - enterTime > timeToKill)) {
            GameController.Instance.GameOver();
        }
    }
}

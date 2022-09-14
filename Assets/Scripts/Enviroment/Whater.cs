using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Whater : MonoBehaviour
{
    [SerializeField] private GameObject WhaterParticle;
    [SerializeField] private float whaterFallSpeed = 2f;
    [SerializeField] private float xDeceleration = 5f;
    AudioSource audioSource;

    private bool playerHeadEntered;
    private bool playerBottomEntered;

    private void Awake() {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D collision) {

        if (collision.gameObject.GetComponent<Player>() && collision.gameObject.GetComponent<Rigidbody2D>().velocity.magnitude > 3) {
            Instantiate(WhaterParticle, collision.gameObject.transform.position, Quaternion.identity);
            audioSource.Play();
        }
            

        if (collision.gameObject.tag == "PlayerHeadSpotter" || collision.gameObject.tag == "BouncingTip") {

            if(collision.gameObject.tag == "PlayerHeadSpotter")
                playerHeadEntered = true;
            else
                playerBottomEntered = true;

            if (playerHeadEntered && playerBottomEntered) GameController.Instance.GameOver();

        }
    }
    private void OnTriggerExit2D(Collider2D collision) {
        if (collision.gameObject.tag == "PlayerHeadSpotter")
            playerHeadEntered = false;
        else if (collision.gameObject.tag == "BouncingTip")
            playerBottomEntered = false;
    }

    private void OnTriggerStay2D(Collider2D collision) {
        Rigidbody2D otherRb = collision.gameObject.GetComponent<Rigidbody2D>();
        if (otherRb) {
            float newXSpeed = otherRb.velocity.x + (-Mathf.Sign(otherRb.velocity.x) * xDeceleration * Time.deltaTime);
            otherRb.velocity = new Vector2(newXSpeed, -whaterFallSpeed);
        }
    }


}

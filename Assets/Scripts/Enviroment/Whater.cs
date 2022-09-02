using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Whater : MonoBehaviour
{
    [SerializeField] private GameObject WhaterParticle;
    [SerializeField] private AudioClip WhaterSound;

    private bool playerHeadEntered;
    private bool playerBottomEntered;

    private void OnTriggerEnter2D(Collider2D collision) {

        if(collision.gameObject.tag == "PlayerHeadSpotter" || collision.gameObject.tag == "BouncingTip") {
            Instantiate(WhaterParticle, collision.gameObject.transform.position, Quaternion.identity);
            AudioManager.Instance.PlaySound(WhaterSound);
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


}

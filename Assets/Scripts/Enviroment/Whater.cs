using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Whater : MonoBehaviour
{
    [SerializeField] private GameObject WhaterParticle;
    [SerializeField] private AudioClip WhaterSound;
    [SerializeField] private Sprite DeathSprite;

    private bool playerHeadEntered;
    private bool playerBottomEntered;

    private void OnTriggerEnter2D(Collider2D collision) {

        if (collision.gameObject.tag == "PlayerHeadSpotter" || collision.gameObject.tag == "BouncingTip")
        {
            if (playerBottomEntered == false && playerHeadEntered == false)
            {
                GameObject player = GameObject.Find("Player");
                player.GetComponent<Rigidbody2D>().gravityScale = 0.3f;
                player.GetComponent<Rigidbody2D>().velocity = new Vector2(player.GetComponent<Rigidbody2D>().velocity.x, player.GetComponent<Rigidbody2D>().velocity.y * 0.5f);
                //player.GetComponent<Animator>().SetBool("IsDead", true);
                player.GetComponent<Animator>().enabled = false;
                GameObject.Find("Hammer").GetComponentInParent<Animator>().enabled = false;
                player.GetComponent<SpriteRenderer>().sprite = DeathSprite;
                Instantiate(WhaterParticle, collision.gameObject.transform.position, Quaternion.identity);
                AudioManager.Instance.PlaySound(WhaterSound);
            }
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

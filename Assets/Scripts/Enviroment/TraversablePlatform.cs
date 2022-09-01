using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TraversablePlatform : MonoBehaviour {

    [SerializeField] private GameObject topPosition;

    BoxCollider2D myCollider;
    BoxCollider2D myTrigger;

    private void Awake() {
        BoxCollider2D [] boxColliders = GetComponents<BoxCollider2D>();

        foreach (BoxCollider2D bc in boxColliders) {
            if (bc.isTrigger) myTrigger = bc;
            else myCollider = bc;
        }
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        myCollider.size = new Vector2(spriteRenderer.size.x, spriteRenderer.size.y);
        myTrigger.size = new Vector2(spriteRenderer.size.x + 0.2f, spriteRenderer.size.y+0.2f);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Player otherPlayer = other.transform.root.GetComponent<Player>();
        if ((otherPlayer != null && otherPlayer.IsPerformingButtHit())
            ||
            (other.gameObject.transform.root.GetComponent<Rigidbody2D>() != null &&
            other.gameObject.transform.root.GetComponent<Rigidbody2D>().velocity.y > 0 &&
            other.transform.position.y < topPosition.transform.position.y &&
            canBeDisabled)
            )
        {
            myCollider.enabled = false;
        }
        else {
            myCollider.enabled = true;
            //if (otherPlayer == null) Debug.Log("other player is null");
            //else Debug.Log("not performing butt hit");
        }  
    }

    //prevents pekan block on bars
    private void OnCollisionStay2D(Collision2D collision) {
        Player player = collision.gameObject.GetComponent<Player>();
        if (player) {
            foreach(ContactPoint2D c in collision.contacts) {
                if (c.point.y < transform.position.y) myCollider.enabled = false;
            }
        }
    }

    private bool canBeDisabled = true;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
            canBeDisabled = false;
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
            canBeDisabled = true;
    }

}



using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TraversablePlatform : MonoBehaviour {

    [SerializeField] private GameObject topPosition;

    BoxCollider2D boxCollider;
    private void Awake() {
        boxCollider = GetComponent<BoxCollider2D>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Player otherPlayer = other.transform.root.GetComponent<Player>();
        if ((otherPlayer != null && otherPlayer.IsPerformingButtHit()) ||
            other.gameObject.transform.root.GetComponent<Rigidbody2D>() != null &&
            other.gameObject.transform.root.GetComponent<Rigidbody2D>().velocity.y > 0 &&
            other.transform.position.y < topPosition.transform.position.y &&
            canBeDisabled)
        {
            boxCollider.enabled = false;
        }
        else
        {
            if( otherPlayer == null || !otherPlayer.IsPerformingButtHit())
            {
                boxCollider.enabled = true;
                //if (otherPlayer == null) Debug.Log("other player is null");
                //else Debug.Log("not performing butt hit");
            }
               
        }
            
    }

    private bool canBeDisabled = true;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
            canBeDisabled = false;
        //Debug.Log(canBeDisabled);
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
            canBeDisabled = true;
        //Debug.Log(canBeDisabled);
    }

}



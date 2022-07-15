using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TraversablePlatform : MonoBehaviour {

    BoxCollider2D boxCollider;
    private void Awake() {
        boxCollider = GetComponent<BoxCollider2D>();
    }

    private void OnTriggerEnter2D(Collider2D other) {

        switch (other.gameObject.tag) {
            case "Player":
                if (other.gameObject.GetComponent<Rigidbody2D>().velocity.y > 0) {
                    boxCollider.enabled = false;
                }
                else
                    boxCollider.enabled = true;
                break;
            case "BouncingTip":
                Debug.Log(other.gameObject.transform.parent.gameObject.name);
                if (other.gameObject.transform.parent.gameObject.GetComponent<Rigidbody2D>().velocity.y > 0) {
                    boxCollider.enabled = false;
                }
                else
                    boxCollider.enabled = true;
                break;
        }
    }

}



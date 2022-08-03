using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TraversablePlatform : MonoBehaviour {

    BoxCollider2D boxCollider;
    private void Awake() {
        boxCollider = GetComponent<BoxCollider2D>();
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.transform.root.GetComponent<Rigidbody2D>().velocity.y > 0 || other.transform.position.y < transform.position.y)
        {
            boxCollider.enabled = false;
        }
        else
            boxCollider.enabled = true;
    }

}



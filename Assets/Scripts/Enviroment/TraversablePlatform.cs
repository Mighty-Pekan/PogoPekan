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
        Debug.Log(other.transform.position);
        if (other.gameObject.transform.root.GetComponent<Rigidbody2D>() != null && 
            other.gameObject.transform.root.GetComponent<Rigidbody2D>().velocity.y > 0 ||
            other.transform.position.y < topPosition.transform.position.y)
            boxCollider.enabled = false;
        else
            boxCollider.enabled = true;
    }

}



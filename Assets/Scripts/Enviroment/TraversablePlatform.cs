using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TraversablePlatform : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D other)
    {
            Debug.Log("Sono qui " + other.gameObject.name);

        if (other.collider.gameObject.transform.CompareTag("PlayerBody"))
        {
            Physics2D.IgnoreCollision(other.collider, GetComponent<Collider2D>(), true);
        }
        else
        {
        }
    }
}

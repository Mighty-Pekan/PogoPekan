using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBody : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D other) {

        if (other.collider.gameObject.tag == "Thorns") {
            GameController.GameOver();
        }
    }
}

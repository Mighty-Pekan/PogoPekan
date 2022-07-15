using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathBox : MonoBehaviour
{
        private void OnCollisionEnter2D(Collision2D other) {

        if (other.collider.gameObject.CompareTag("Player")) {
            GameController.GameOver();
        }
    }
}

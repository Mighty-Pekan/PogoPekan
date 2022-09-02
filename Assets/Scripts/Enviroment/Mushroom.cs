using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mushroom : MonoBehaviour
{
    [SerializeField] float bounciness;
    [SerializeField] AudioClip BounceSound;

    public float GetBounciness() {
        return bounciness;
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if(collision.gameObject.tag == "Player") {
            AudioManager.Instance.PlaySound(BounceSound);
        }
    }

}

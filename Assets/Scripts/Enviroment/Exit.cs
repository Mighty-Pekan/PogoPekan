using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exit : MonoBehaviour
{
    [SerializeField] AudioClip ExitReachedSound;
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.tag == "Player") {
            AudioManager.Instance.PlaySound(ExitReachedSound);
            GameController.Instance.ExitReached();
        }
            
    }


}

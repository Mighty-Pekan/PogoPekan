using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEmitter : MonoBehaviour
{
    [SerializeField] AudioClip[] possibleSounds;

    private void OnCollisionEnter2D(Collision2D collision) {
        if(collision.gameObject.tag == "Player" && possibleSounds.Length >0) {
            int r = Random.Range(0, possibleSounds.Length);
            //Debug.Log(r);
            AudioManager.Instance.PlaySound(possibleSounds[r]);
        }
    }
}

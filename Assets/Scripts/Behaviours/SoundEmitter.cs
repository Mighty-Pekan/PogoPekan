using Cinemachine.Utility;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEmitter : MonoBehaviour
{
    [SerializeField] private AudioClip[] possibleSounds;
    private AudioSource audioSource;
    private float? lastTimeSound = null;
    private float minTimeBetweenSounds = 0.2f;

    private void Awake() {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if(collision.gameObject.tag == "Player" && possibleSounds.Length >0 && GameController.Instance.GetPlayer().IsAlive) {
            if(collision.collider.gameObject.tag == "BouncingTip" || collision.relativeVelocity.magnitude > 6){
                int r = Random.Range(0, possibleSounds.Length);
                audioSource.PlayOneShot(possibleSounds[r]);
                lastTimeSound = Time.time;
            }
        }
    }
}

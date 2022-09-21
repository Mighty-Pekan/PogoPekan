using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakablePlatformButtHit : GenericSlicedPlatform
{
    [SerializeField] GameObject destructionParticles;
    AudioSource audioSource;

    private void Awake() {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.collider.gameObject.tag == "BouncingTip" && GameController.Instance.GetPlayer().IsPerformingButtHitDive()) {
            Instantiate(destructionParticles, collision.contacts[0].point,Quaternion.identity);
            AudioManager.Instance.ConcreteBreakAudioSource.Play();
            GameController.Instance.GetPlayer().ContinueDive();
            Destroy(gameObject);
        }
    }



}

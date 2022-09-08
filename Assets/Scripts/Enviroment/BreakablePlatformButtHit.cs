using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakablePlatformButtHit : MonoBehaviour
{
    [SerializeField] GameObject destructionParticles;
    AudioSource audioSource;

    int life = 4;
    int tickDamage = 1;

    private SpriteRenderer mySpriteRenderer;
    private int quarterOfLife;
    private float? lastTimeTookDamage = null;



    private void Awake() {
        audioSource = GetComponent<AudioSource>();
    }

    private void Start() {
        quarterOfLife = life / 4;
        mySpriteRenderer = GetComponent<SpriteRenderer>();
        GetComponent<BoxCollider2D>().size = new Vector2(mySpriteRenderer.size.x, mySpriteRenderer.size.y);
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrayFish : MonoBehaviour
{
    [SerializeField] GameObject fishParticle;

    private void OnTriggerEnter2D(Collider2D collision) {
        AudioManager.Instance.FishGotAudioSource.Play();
        Instantiate(fishParticle, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}

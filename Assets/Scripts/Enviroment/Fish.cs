using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fish : MonoBehaviour
{
    public enum FishId {ONE = 1,TWO =2};
    [SerializeField] FishId id = FishId.ONE;
    [SerializeField] AudioClip fishCatchSound;
    [SerializeField] GameObject fishParticle;
    AudioSource audioSource;

    private void Awake() {
        audioSource = GetComponent<AudioSource>();
    }

    private void Start() {
        if(LevelsDataManager.Instance.IsFishFound(id))Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        LevelsDataManager.Instance.SetFishFound(id);
        audioSource.Play();
        Instantiate(fishParticle, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

}

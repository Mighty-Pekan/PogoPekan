using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericAudioSource : MonoBehaviour
{
    AudioSource audioSource;

    private void Awake() {
        audioSource = GetComponent<AudioSource>();
    }

    public void Play() {
        audioSource.Play();
    }
    public void Stop() {
        audioSource.Stop(); 
    }
}

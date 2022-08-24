using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoSingleton<AudioManager> {

    [SerializeField] private AudioSource sfxAudioSource;
    [SerializeField] private AudioSource musicAudioSource;
    [SerializeField] private AudioClip menuTrack;
    [SerializeField] private AudioClip[] musicTracks;
    private int lastMusicPlayed;

    // Start is called before the first frame update
    void Start() {
        StartCoroutine(playMusic());
    }

    public void PlaySound(AudioClip _audio) {
        sfxAudioSource.PlayOneShot(_audio, PlayerPrefs.GetFloat("SfxVolume"));
    }

    public void ChangeMusic() {
        StopAllCoroutines();
        musicAudioSource.Stop();
        StartCoroutine(playMusic());
    }

    private IEnumerator playMusic() {

        yield return new WaitForSeconds(0.1f);

        if (GameController.Instance.isStartMenu()) {
            Debug.Log("is start menu");
            while (true) {
                yield return new WaitForSeconds(0.2f);
                if (!musicAudioSource.isPlaying) {
                    musicAudioSource.PlayOneShot(menuTrack, PlayerPrefs.GetFloat("MusicVolume"));
                }
            }
        }
        else {
            int random = Random.Range(0, musicTracks.Length);
            lastMusicPlayed = random;
            musicAudioSource.PlayOneShot(musicTracks[random], PlayerPrefs.GetFloat("MusicVolume"));
            while (true) {
                yield return new WaitForSeconds(0.2f);
                if (!musicAudioSource.isPlaying) {
                    musicAudioSource.PlayOneShot(musicTracks[random], PlayerPrefs.GetFloat("MusicVolume"));
                }
            }
        } 
    }

    public void refreshMusicVolume() {
        musicAudioSource.volume = PlayerPrefs.GetFloat("MusicVolume");
    }
}
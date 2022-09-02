using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoSingleton<AudioManager> {

    [SerializeField] private AudioSource sfxAudioSource;
    [SerializeField] private AudioSource musicAudioSource;
    [SerializeField] private AudioSource superjumpAudioSource;
    [SerializeField] private AudioClip menuTrack;
    [SerializeField] private AudioClip[] musicTracks;
    [SerializeField] int? lastSongIndex = null;
    [SerializeField] int? currentSongIndex = null;

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

    public void StopMusic() {
        StopAllCoroutines();
        musicAudioSource.Stop();
    }

    private IEnumerator playMusic() {

        yield return new WaitForSeconds(0.1f);

        if (GameController.Instance.isStartMenu()) {
            while (true) {
                yield return new WaitForSeconds(0.2f);
                if (!musicAudioSource.isPlaying) {
                    musicAudioSource.PlayOneShot(menuTrack, PlayerPrefs.GetFloat("MusicVolume"));
                }
            }
        }
        else {
            while(currentSongIndex == lastSongIndex || currentSongIndex == null) {
                currentSongIndex = Random.Range(0, musicTracks.Length);
                //Debug.Log("random: " + newSongIndex);
            }
            //Debug.Log("new song index:" + newSongIndex);
            lastSongIndex = currentSongIndex;
            //musicAudioSource.PlayOneShot(musicTracks[newSongIndex], PlayerPrefs.GetFloat("MusicVolume"));
            while (true) {
                if (!musicAudioSource.isPlaying) {
                    musicAudioSource.PlayOneShot(musicTracks[(int)currentSongIndex], PlayerPrefs.GetFloat("MusicVolume"));
                }
                yield return new WaitForSeconds(0.2f);
            }
        } 
    }

    public void refreshMusicVolume() {
        musicAudioSource.volume = PlayerPrefs.GetFloat("MusicVolume");
    }

    //================================================================================== superjump sound
    //public void initSuperjumpAudioClip(AudioClip _audio) {
    //    superjumpAudioSource.clip = _audio;
    //}
    public void PlayInterruptableSound(AudioClip _audio) {
        superjumpAudioSource.clip = _audio;
        superjumpAudioSource.volume = PlayerPrefs.GetFloat("SfxVolume");
        superjumpAudioSource.Play();
    }
    public void StopInterruptableSound() {
        //Debug.Log("superjump audio stopped");
        superjumpAudioSource.Stop();
    }
}
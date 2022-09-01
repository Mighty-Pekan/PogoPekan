using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoSingleton<AudioManager> {

    [SerializeField] private AudioSource sfxAudioSource;
    [SerializeField] private AudioSource musicAudioSource;
    [SerializeField] private AudioSource superjumpAudioSource;
    [SerializeField] private AudioClip menuTrack;
    [SerializeField] private AudioClip[] musicTracks;
    private int? lastSongIndex = null;

    // Start is called before the first frame update
    void Start() {
        StartCoroutine(playMusic());
    }

    public void PlaySound(AudioClip _audio) {
        sfxAudioSource.PlayOneShot(_audio, PlayerPrefs.GetFloat("SfxVolume"));
    }

    public void initSuperjumpAudioClip(AudioClip _audio) {
        superjumpAudioSource.clip = _audio;
    }
    public void PlaySuperjumpSound(AudioClip _audio) {
        superjumpAudioSource.volume = PlayerPrefs.GetFloat("SfxVolume");
        superjumpAudioSource.Play();
    }
    public void StopSuperjumpSound() {
        //Debug.Log("superjump audio stopped");
        superjumpAudioSource.Stop();        
    }

    public void ChangeMusic() {
        StopAllCoroutines();
        musicAudioSource.Stop();
        StartCoroutine(playMusic());
    }

    private IEnumerator playMusic() {

        yield return new WaitForSeconds(0.2f);

        if (GameController.Instance.isStartMenu()) {
            while (true) {
                yield return new WaitForSeconds(0.2f);
                if (!musicAudioSource.isPlaying) {
                    musicAudioSource.PlayOneShot(menuTrack, PlayerPrefs.GetFloat("MusicVolume"));
                }
            }
        }
        else {
            if(lastSongIndex!=null)Debug.Log("last song index: " + lastSongIndex);

            int? newSongIndex = null;
            while(newSongIndex == lastSongIndex || newSongIndex == null) {
                newSongIndex = Random.Range(0, musicTracks.Length);
                //Debug.Log("random: " + newSongIndex);
            }
            //Debug.Log("new song index:" + newSongIndex);
            lastSongIndex = newSongIndex;
            //musicAudioSource.PlayOneShot(musicTracks[newSongIndex], PlayerPrefs.GetFloat("MusicVolume"));
            while (true) {
                if (!musicAudioSource.isPlaying) {
                    musicAudioSource.PlayOneShot(musicTracks[(int)newSongIndex], PlayerPrefs.GetFloat("MusicVolume"));
                }
                yield return new WaitForSeconds(0.2f);
            }
        } 
    }

    public void refreshMusicVolume() {
        musicAudioSource.volume = PlayerPrefs.GetFloat("MusicVolume");
    }
}
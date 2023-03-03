using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoSingleton<AudioManager> {

    [SerializeField] private AudioSource musicAudioSource;
    [SerializeField] private AudioClip menuTrack;
    [SerializeField] private AudioClip[] musicTracks;
    [SerializeField] int? lastSongIndex = null;
    [SerializeField] int? currentSongIndex = null;
    [SerializeField] AudioMixer audioMixer;

    [Header("Audiosources")]
    [SerializeField] public GenericAudioSource GameoverAudioSource;
    [SerializeField] public GenericAudioSource ConcreteBreakAudioSource;
    [SerializeField] public GenericAudioSource FishGotAudioSource;

    // Start is called before the first frame update
    void Start() {
        InitMixerVolume();
        StartCoroutine(playMusic());
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

        if (GameController.Instance.IsMainMenu()) {
            while (true) {
                yield return new WaitForSeconds(0.2f);
                if (!musicAudioSource.isPlaying) {
                    musicAudioSource.PlayOneShot(menuTrack,0.5f);
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
                    musicAudioSource.PlayOneShot(musicTracks[(int)currentSongIndex],0.5f);
                }
                yield return new WaitForSeconds(0.2f);
            }
        } 
    }

    public void refreshMusicVolume() {
        musicAudioSource.volume = PlayerPrefs.GetFloat("MusicVolume");
    }

    public void SetMixerVolume(string volumeKey, float value) {
        if(volumeKey == GameController.Instance.SFX_VOLUME_KEY)
            audioMixer.SetFloat(volumeKey, Mathf.Log10(value) * 20 +20);
        else 
            audioMixer.SetFloat(volumeKey, Mathf.Log10(value) * 20);
    }

    private void InitMixerVolume() {

        List<string> keys = new List<string>();
        keys.Add(GameController.Instance.MASTER_VOLUME_KEY);
        keys.Add(GameController.Instance.MUSIC_VOLUME_KEY);
        keys.Add(GameController.Instance.SFX_VOLUME_KEY);

        foreach (string key in keys) {
            if (!PlayerPrefs.HasKey(key)) {
                if (key == GameController.Instance.MASTER_VOLUME_KEY) {
                    PlayerPrefs.SetFloat(key, 0.5f);
                }
                else if (key == GameController.Instance.MUSIC_VOLUME_KEY) {
                    PlayerPrefs.SetFloat(key, 0.5f);
                }
                else {
                    PlayerPrefs.SetFloat(key, 1f);
                }
                    
            }
            float value = PlayerPrefs.GetFloat(key);
            SetMixerVolume(key, value);
        }
    }

}
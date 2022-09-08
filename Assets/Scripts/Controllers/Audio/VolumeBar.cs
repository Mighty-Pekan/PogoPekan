using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeBar : MonoBehaviour {
    Slider slider;
    private enum BarType {MASTER,MUSIC,SFX };
    string volumeKey;

    [SerializeField] private BarType barType = BarType.MASTER;

    private void Start() {

        switch (barType) {
            case BarType.SFX:
                volumeKey = GameController.Instance.SFX_VOLUME_KEY;
                break;
            case BarType.MUSIC:
                volumeKey = GameController.Instance.MUSIC_VOLUME_KEY;
                break;
            case BarType.MASTER:
                volumeKey = GameController.Instance.MASTER_VOLUME_KEY;
                break;
        }

        slider = GetComponent<Slider>();

        if (!PlayerPrefs.HasKey(volumeKey)) {
            if(volumeKey == GameController.Instance.MASTER_VOLUME_KEY)
                PlayerPrefs.SetFloat(volumeKey, 1f);
            if(volumeKey == GameController.Instance.MUSIC_VOLUME_KEY)
                PlayerPrefs.SetFloat(volumeKey, 0.5f);
            else
                PlayerPrefs.SetFloat(volumeKey, 1f);
        }
        LoadVolume();
    }

    private void LoadVolume() {
        slider.value = PlayerPrefs.GetFloat(volumeKey);
    }

    public void SetVolume() {
        PlayerPrefs.SetFloat(volumeKey, slider.value);
        AudioManager.Instance.SetMixerVolume(volumeKey, slider.value);
    }
}

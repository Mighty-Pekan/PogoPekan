using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeBar : MonoBehaviour {
    Slider slider;
    [SerializeField] private string key = "SfxVolume";

    private void Start() {
        slider = GetComponent<Slider>();

        if (!PlayerPrefs.HasKey(key)) {
            PlayerPrefs.SetFloat(key, 0.7f);
        }
        Load();
    }

    private void Load() {
        slider.value = PlayerPrefs.GetFloat(key);
    }

    public void Save() {
        PlayerPrefs.SetFloat(key, slider.value);
        AudioManager.Instance.refreshMusicVolume();
    }
}

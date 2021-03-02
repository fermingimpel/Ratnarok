using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeVolumeSliders : MonoBehaviour {

    [SerializeField] Slider slider;
    [SerializeField] float musicVolume;
    [SerializeField] float soundsVolume;
    [SerializeField] string value;
    GameVolume gv;
    private void Start() {
        gv = FindObjectOfType<GameVolume>();
        musicVolume = gv.MusicVolume;
        soundsVolume = gv.SFXVolume;
        if (value == "Music") 
            slider.value = musicVolume / 100f;
        else 
            slider.value = soundsVolume / 100f;
  
        AkSoundEngine.SetRTPCValue("music_option", musicVolume);
        AkSoundEngine.SetRTPCValue("sfx_option", soundsVolume);
    }

    public void SetSpecificVolume() {
        if (value == "Music") {
            musicVolume = slider.value * 100;
            AkSoundEngine.SetRTPCValue("music_option", musicVolume);
            gv.MusicVolume = musicVolume;
        }
        else {
            soundsVolume = slider.value * 100;
            AkSoundEngine.SetRTPCValue("sfx_option", soundsVolume);
            gv.SFXVolume = soundsVolume;
        }
    }

}

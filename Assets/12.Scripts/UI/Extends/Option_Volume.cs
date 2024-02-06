using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class Option_Volume : MonoBehaviour
{
    public AudioMixer mixer;

    [Header("Sliders")]
    public Slider MasterSlider;
    public Slider EffectSlider;
    public Slider MusicSlider;

    private void Start()
    {
        MasterSlider.value = Managers.Sound.MasterVolume;
        EffectSlider.value = Managers.Sound.SFXVolume;
        MusicSlider.value = Managers.Sound.BGMVolume;
    }

    public void SetMasterVol(float sliderVal)
    {
        mixer.SetFloat("MyVolume", Mathf.Log10(sliderVal) * 20);

        Managers.Sound.MasterVolume = sliderVal;
        Managers.Data.SaveSoundData();
    }
    public void SetSFXVol(float sliderVal)
    {
        mixer.SetFloat("MySFX", Mathf.Log10(sliderVal) * 20);

        Managers.Sound.SFXVolume = sliderVal;
        Managers.Data.SaveSoundData();
    }
    public void SetMusicVol(float sliderVal)
    {
        mixer.SetFloat("MyMusic", Mathf.Log10(sliderVal) * 20);

        Managers.Sound.BGMVolume = sliderVal;
        Managers.Data.SaveSoundData();
    }

    public void ClickSoundPlay()
    {
        Managers.Sound.PlaySFX(SFX.DefaultButton);
    }
}

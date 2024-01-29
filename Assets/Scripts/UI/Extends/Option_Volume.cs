using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class Option_Volume : MonoBehaviour
{
    public AudioMixer mixer;
    public void SetMasterVol(float sliderVal)
    {
        mixer.SetFloat("MyVolume", Mathf.Log10(sliderVal) * 20);
    }
    public void SetSFXVol(float sliderVal)
    {
        mixer.SetFloat("MySFX", Mathf.Log10(sliderVal) * 20);
    }
    public void SetMusicVol(float sliderVal)
    {
        mixer.SetFloat("MyMusic", Mathf.Log10(sliderVal) * 20);
    }
}

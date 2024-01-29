using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.InputSystem;

public class SoundManager
{
    [HideInInspector]
    public AudioSource AudioSourceBGM;
    public AudioSource AudioSourceSFX;

    private Dictionary<SFX, AudioClip> _sfx = new Dictionary<SFX, AudioClip>();
    private Dictionary<BGM, AudioClip> _bgm = new Dictionary<BGM, AudioClip>();


    private float _masterVolume = 1f;
    private float _volumeSFX = 1f;
    private float _volumeBGM = 1f;

    public float MasterVolume
    {
        get
        {
            return _masterVolume;
        }
        set
        {
            _masterVolume = value;
            SetVolumeBGM();
            SetVolumeSFX();
        }
    }

    public float SFXVolume
    {
        get
        {
            return _volumeSFX;
        }
        set
        {
            _volumeSFX = value;
            SetVolumeSFX();
        }
    }

    public float BGMVolume
    {
        get
        {
            return _volumeBGM;
        }
        set
        {
            _volumeBGM = value;
            SetVolumeBGM();
        }
    }

    public void Initialized()
    {
        _sfx.Add(SFX.Attack, Managers.Resource.Load<AudioClip>("AttackSound"));
        _sfx.Add(SFX.Skill, Managers.Resource.Load<AudioClip>("SkillSound"));
        _sfx.Add(SFX.FirstWalk, Managers.Resource.Load<AudioClip>("FirstGrassWalk"));
        _sfx.Add(SFX.SecondWalk, Managers.Resource.Load<AudioClip>("SecondGrassWalk"));

        _bgm.Add(BGM.Stage0, Managers.Resource.Load<AudioClip>("Stage0BGM"));
        _bgm.Add(BGM.Stage1, Managers.Resource.Load<AudioClip>("Stage1BGM"));
        _bgm.Add(BGM.Lobby2, Managers.Resource.Load<AudioClip>("Lobby2"));
    }

    public IEnumerator VolumeDown()
    {
        while (AudioSourceBGM.volume > 0)
        {
            AudioSourceBGM.volume -= Time.deltaTime / 4;        // 4초에 걸쳐 줄어들도록
            Debug.Log("Volume Down");
            yield return null;
        }
        Managers.Game.CallStageEnd();
    }

    public float PlayTime()
    {
        return AudioSourceBGM.time;
    }

    public void PlaySFX(SFX key, float volumeScale = 1f)
    {
        AudioSourceSFX.PlayOneShot(_sfx[key], volumeScale * _volumeSFX * _masterVolume);
    }

    private void SetBGM(BGM key)
    {
        AudioSourceBGM.Stop();
        AudioSourceBGM.volume = _masterVolume * _volumeBGM;
        AudioSourceBGM.clip = _bgm[key];
    }

    public void PlayBGM(BGM key)
    {
        AudioSourceBGM.loop = false;
        SetBGM(key);
        AudioSourceBGM.Play();
        AudioSourceBGM.loop = true;
    }

    public void LoopPlayBGM(BGM key)
    {
        AudioSourceBGM.loop = true;
        SetBGM(key);
        AudioSourceBGM.Play();
    }

    public void DelayedPlayBGM(BGM key, float delay)
    {
        AudioSourceBGM.loop = true;
        SetBGM(key);
        AudioSourceBGM.PlayDelayed(delay);
    }

    private void SetVolumeBGM()
    {
        AudioSourceBGM.volume = _masterVolume * _volumeBGM;
    }
    
    private void SetVolumeSFX()
    {
        AudioSourceSFX.volume = _masterVolume * _volumeSFX;
    }

    public void PauseBGM()
    {
        AudioSourceBGM?.Pause();
    }

    public void ContinueBGM()
    {
        AudioSourceBGM?.UnPause();
    }

    public void StopBGM()
    {
        AudioSourceBGM?.Stop();
    }
}

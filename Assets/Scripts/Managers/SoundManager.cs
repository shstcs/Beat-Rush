using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class SoundManager
{
    [HideInInspector]
    public AudioSource AudioSource;

    private Dictionary<SFX, AudioClip> _sfx = new Dictionary<SFX, AudioClip>();
    private Dictionary<BGM, AudioClip> _bgm = new Dictionary<BGM, AudioClip>();


    private float _masterVolume = 1f;
    private float _SFXVolume = 1f;
    private float _BGMVolume = 1f;

    public float MasterVolume
    {
        get
        {
            return _masterVolume;
        }
        set
        {
            _masterVolume = value;
        }
    }

    public float SFXVolume
    {
        get
        {
            return _SFXVolume;
        }
        set
        {
            _SFXVolume = value;
        }
    }

    public float BGMVolume
    {
        get
        {
            return _BGMVolume;
        }
        set
        {
            _BGMVolume = value;
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
        while (AudioSource.volume > 0)
        {
            AudioSource.volume -= Time.deltaTime / 4;        // 4초에 걸쳐 줄어들도록
            Debug.Log("Volume Down");
            yield return null;
        }
        Managers.Game.CallStageEnd();
    }

    public float PlayTime()
    {
        return AudioSource.time;
    }

    public void PlaySFX(SFX key, float volumeScale = 1f)
    {
        AudioSource.PlayOneShot(_sfx[key], volumeScale * _SFXVolume * _masterVolume);
    }

    private void SetBGM(BGM key)
    {
        AudioSource.Stop();
        AudioSource.volume = _masterVolume * _BGMVolume;
        AudioSource.clip = _bgm[key];
    }

    public void PlayBGM(BGM key)
    {
        AudioSource.loop = false;
        SetBGM(key);
        AudioSource.Play();
        AudioSource.loop = true;
    }

    public void LoopPlayBGM(BGM key)
    {
        AudioSource.loop = true;
        SetBGM(key);
        AudioSource.Play();
    }

    public void DelayedPlayBGM(BGM key, float delay)
    {
        AudioSource.loop = true;
        SetBGM(key);
        AudioSource.PlayDelayed(delay);
    }

    public void PauseBGM()
    {
        AudioSource?.Pause();
    }

    public void ContinueBGM()
    {
        AudioSource?.UnPause();
    }

    public void StopBGM()
    {
        AudioSource?.Stop();
    }
}

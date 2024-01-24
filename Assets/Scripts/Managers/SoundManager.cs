using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [HideInInspector]
    public AudioSource AudioSource;

    private Dictionary<SFX, AudioClip> _sfx = new Dictionary<SFX, AudioClip>();
    private Dictionary<BGM, AudioClip> _bgm = new Dictionary<BGM, AudioClip>();



    public void Initialized()
    {
        _sfx.Add(SFX.Attack, Managers.Resource.Load<AudioClip>("AttackSound"));
        _sfx.Add(SFX.Skill, Managers.Resource.Load<AudioClip>("SkillSound"));

        _bgm.Add(BGM.Stage1, Managers.Resource.Load<AudioClip>("Stage1BGM"));
        _bgm.Add(BGM.Lobby2, Managers.Resource.Load<AudioClip>("Lobby2"));
    }

    public void PlayClip(float delay)
    {
        AudioSource?.PlayDelayed(delay);
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

    public void PlaySFX(SFX key)
    {
        AudioSource.PlayOneShot(_sfx[key]);
    }
    
    public void PlayBGM(BGM key)
    {
        AudioSource.volume = 0.2f;
        AudioSource.Stop();
        AudioSource.clip = _bgm[key];
        AudioSource.Play();
    }

    public void DelayedPlayBGM(BGM key,float delay)
    {
        AudioSource.Stop();
        AudioSource.clip = _bgm[key];
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

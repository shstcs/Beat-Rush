using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    private AudioSource _audioSource;
    private AudioClip _stage1Clip;

    private Dictionary<string, AudioClip> _sfx = new Dictionary<string, AudioClip>();

    private void Awake()
    {
        _audioSource = gameObject.AddComponent<AudioSource>();
        _stage1Clip = Resources.Load<AudioClip>("Stage1BGM");
        _audioSource.clip = _stage1Clip;
    }

    private void Start()
    {
        Initialized();
    }

    private void Initialized()
    {
        _sfx.Add("PlayerAttack", Resources.Load<AudioClip>("Sound/Effect/AttackSound"));
        _sfx.Add("PlayerSkill", Resources.Load<AudioClip>("Sound/Effect/SkillSound"));
    }

    public void PlayClip(float delay)
    {
        _audioSource?.PlayDelayed(delay);
    }

    public IEnumerator VolumeDown()
    {
        while (_audioSource.volume > 0)
        {
            _audioSource.volume -= Time.deltaTime / 4;        // 4초에 걸쳐 줄어들도록
            Debug.Log("Volume Down");
            yield return null;
        }
        Managers.Game.CallStageEnd();
    }

    public float PlayTime()
    {
        return _audioSource.time;
    }

    public void PlaySFX(string key)
    {
        _audioSource.PlayOneShot(_sfx[key]);
    }
}

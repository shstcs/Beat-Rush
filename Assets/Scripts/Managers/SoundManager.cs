using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    private AudioSource _audioSource;
    private AudioClip _stage1Clip;

    private void Awake()
    {
        _audioSource = gameObject.AddComponent<AudioSource>();
        _stage1Clip = Resources.Load<AudioClip>("Stage1BGM");
        _audioSource.clip = _stage1Clip;
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

}

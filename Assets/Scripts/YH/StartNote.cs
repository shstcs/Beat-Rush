using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartNote : Note
{
    //TODO: 노트 매니저에 접근해서 음악 재생하게 변경
    [SerializeField] private AudioSource _music;
    private float _bpm = 72;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Line"))
        {
            _music.PlayDelayed(20 * (60 / _bpm) / 5);  // 노트와의 거리 20 * 1m 당 속도
            Destroy(gameObject);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartNote : Note
{
    //TODO: 노트 매니저에 접근해서 음악 재생하게 변경
    [SerializeField] private SoundManager _soundManager;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Line"))
        {
            //_music.PlayDelayed(20 * (60 / Managers.Game.bpm) / 5 +1.6f);  // 노트와의 거리 20 * 1m 당 속도
            _soundManager.PlayClip(20 * (60 / Managers.Game.bpm) / 5 + 1.6f);
            Destroy(gameObject);
        }
    }
}

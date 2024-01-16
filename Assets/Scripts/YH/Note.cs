using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Note : MonoBehaviour
{
    private ParticleSystem _particle;
    private float _noteDistance = 5;
    private float _noteSpeed;
    private double _curDsp;

    protected void Awake()
    {
        _particle = Resources.Load<ParticleSystem>("Blood Splash");
    }

    private void Start()
    {
        _particle.Stop();
        _noteSpeed = _noteDistance / (60 / Managers.Game.bpm);
        _curDsp = AudioSettings.dspTime;
    }

    private void Update()
    {
        if(Time.timeScale > 0)
        {
            // 놓친 노트 파괴
            if (gameObject.transform.position.z < 8)
            {
                Managers.Game.Combo = 0;
                BreakNote();
                Managers.Player.ChangeHealth(-1);
                // TODO : 플레이어 피격 설정
            }
            else
            {
                // 노트 이동
                transform.position = new Vector3(transform.position.x, transform.position.y,
                transform.position.z - ((float)(AudioSettings.dspTime - _curDsp) * _noteSpeed));
                _curDsp = AudioSettings.dspTime;
            }
        }
    }

    private void OnEnable()
    {
        _curDsp = AudioSettings.dspTime;
    }

    public void BreakNote()
    {
        ParticleSystem _destroyParticle = Instantiate(_particle);
        _destroyParticle.transform.position = transform.position;
        _particle.Play();
        transform.position = Vector3.zero;
        gameObject.SetActive(false);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Note : MonoBehaviour
{
    private ParticleSystem _particle;
    private float _noteDistance = 5;
    private float _musicBpm = 72;
    private float _noteSpeed;
    private double _curDsp;

    private Color _curColor;
    protected void Awake()
    {
        _particle = Resources.Load<ParticleSystem>("Blood Splash");
    }

    private void Start()
    {
        _particle.Stop();
        _noteSpeed = _noteDistance / (60 / _musicBpm);
        _curDsp = AudioSettings.dspTime;
    }

    private void Update()
    {
        // 놓친 노트 파괴
        if (transform.position.z < -15)
        {
            Destroy(gameObject);
            // TODO : 플레이어 피격 설정
        }

        //노트 이동
        transform.position = new Vector3(transform.position.x, transform.position.y, 
            transform.position.z - ((float)(AudioSettings.dspTime - _curDsp) * _noteSpeed));
        _curDsp = AudioSettings.dspTime;

        //노트 타이밍 가시화
        if(transform.position.z < 4 && transform.position.z >0)
        {
            _curColor = gameObject.GetComponent<MeshRenderer>().material.color;
            gameObject.GetComponent<MeshRenderer>().material.color = Color.Lerp(_curColor, Color.white, 3f * Time.deltaTime);
        }
    }

    private void OnDestroy()
    {
        ParticleSystem _destroyParticle = Instantiate(_particle);
        _destroyParticle.transform.position = transform.position;
        _particle.Play();
    }
}

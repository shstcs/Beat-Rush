using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Note : MonoBehaviour
{
    private bool _isMiss;
    private float _lifeTime;
    private ParticleSystem _particle;
    private float _noteDistance = 5;
    private float _musicBpm = 72;
    private float _noteSpeed;
    private double _curDsp;
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
        if (_isMiss) _lifeTime += Time.deltaTime;
        if (_lifeTime > 2)
        {
            Destroy(gameObject);
        }

        transform.position = new Vector3(transform.position.x, transform.position.y, 
            transform.position.z - ((float)(AudioSettings.dspTime - _curDsp) * _noteSpeed));
        _curDsp = AudioSettings.dspTime;
    }

    private void OnDestroy()
    {
        ParticleSystem _destroyParticle = Instantiate(_particle);
        _destroyParticle.transform.position = transform.position;
        _particle.Play();
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Line"))
        {
            _isMiss = true;
            Destroy(gameObject);
        }
    }
}

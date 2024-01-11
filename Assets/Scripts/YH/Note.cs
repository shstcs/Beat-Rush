using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Note : MonoBehaviour
{
    //public ParticleSystem ParticleSystem;
    private Rigidbody _rigid;

    private bool _isMiss;
    private float _lifeTime;
    private float _noteSpeed = 6f;
    private ParticleSystem _particle;
    protected void Awake()
    {
        _rigid = GetComponent<Rigidbody>();
        _particle = Resources.Load<ParticleSystem>("Blood Splash");
    }

    private void Start()
    {
        _rigid.velocity = new Vector3(0, 0, -_noteSpeed);
        _particle.Stop();
    }
    void Update()
    {
        if(_isMiss) _lifeTime += Time.deltaTime;
        if (_lifeTime > 2)
        {
            Destroy(gameObject);
        }
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
        }
    }
}

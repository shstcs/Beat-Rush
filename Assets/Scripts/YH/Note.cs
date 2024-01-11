using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Note : MonoBehaviour
{
    private Rigidbody _rigid;
    private float _lifeTime;
    private void Awake()
    {
        _rigid = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        _rigid.velocity = new Vector3(0, 0, -5);
    }
    void Update()
    {
        _lifeTime += Time.deltaTime;
        if(_lifeTime > 5)
        {
            Destroy(gameObject);
        }
    }
}

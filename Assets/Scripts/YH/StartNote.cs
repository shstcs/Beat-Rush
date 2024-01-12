using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartNote : Note
{
    [SerializeField] private AudioSource _music;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Line"))
        {
            _music.Play();
            //TODO: 노트 매니저에 접근해서 음악 재생
            Destroy(gameObject);
        }
    }
}

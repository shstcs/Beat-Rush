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
            Destroy(gameObject);
        }
    }

}

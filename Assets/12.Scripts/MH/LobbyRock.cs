using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyRock : MonoBehaviour
{
    Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            ContactPoint cp = collision.contacts[0];
            Vector3 dir = cp.normal + new Vector3(2f, 4f, 2f);
            rb.velocity = dir;
        }
    }
}

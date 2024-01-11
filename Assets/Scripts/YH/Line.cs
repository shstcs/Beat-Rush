using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Line : MonoBehaviour
{
    public LayerMask noteLayer;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            CheckNotes();
        }
    }

    private void CheckNotes()
    {
        Collider[] colliders = Physics.OverlapBox(transform.position, new Vector3(10, 5, 0.02f) / 2, Quaternion.identity, noteLayer);
        if (colliders.Length > 0)
        {
            Debug.Log("Overlab");
            Destroy(colliders[0].gameObject);
        }
        else Debug.Log("no Collider");
    }
}

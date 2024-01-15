using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Line : MonoBehaviour
{
    public LayerMask noteLayer;
    public GameObject Player;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) || Input.GetKeyDown(KeyCode.Z))
        {
            CheckNotes();
        }
    }

    private void CheckNotes()
    {
        Collider[] colliders = Physics.OverlapBox(transform.position, new Vector3(10, 5, 0.02f) / 2, Quaternion.identity, noteLayer);
        if (colliders.Length > 0)
        {
            Player.GetComponent<Player>().Rotate(colliders[0].transform);
            Debug.Log("Overlab");
            colliders[0].GetComponent<Note>().BreakNote();
        }
        else Debug.Log("no Collider");
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteManager : MonoBehaviour
{
    [SerializeField] private GameObject _notePrefab;

    private void Start()
    {
        StartCoroutine(CreateNotes());
    }

    private IEnumerator CreateNotes()
    {
        while (true)
        {
            Instantiate(_notePrefab);
            _notePrefab.transform.position = new Vector3(0, 1, 10);
            yield return new WaitForSeconds(.835f);
        }
    }
}

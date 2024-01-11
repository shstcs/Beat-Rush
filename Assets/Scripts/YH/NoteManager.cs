using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteManager : MonoBehaviour
{
    private GameObject[] _sheet1;
    private int _curBar = 1;
    private List<GameObject> _sheet1Instant;
    //private AudioSource music1;
    //TODO: 음악 실행하는 메서드 만들기

    private void Awake()
    {
        //_sheet1 = Resources.LoadAll<GameObject>("Sheet1");
    }

    private void Start()
    {
        //GameObject go = Instantiate(_sheet1[1]);
        //go.transform.position = new Vector3(-2, 1, 10);
        //go = Instantiate(_sheet1[2]);
        //go.transform.position = new Vector3(-2, 1, 50);
    }

    private void Update()
    {
        
    }

}

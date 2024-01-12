using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteManager : MonoBehaviour
{
    private GameObject[] _sheet1;
    private int _curBar = 1;
    private GameObject _bar;
    public AudioSource music1;          //TODO : 나중에 private으로 바꾸고 매니저를 통해 접근할 예정.

    [Range(0f, 5f)]
    public float latency = 0f;
    [Range(0f, 0.2f)]
    public float instantiateDelay = 0.085f;
    private float _bpm = 72;


    private void Awake()
    {
        _sheet1 = Resources.LoadAll<GameObject>("Sheet1");
    }

    private void Start()
    {
        StartCoroutine(CreateNote());
    }

    private IEnumerator CreateNote()
    {
        while (_curBar < _sheet1.Length)
        {
            _bar = Instantiate(_sheet1[_curBar]);
            _bar.transform.position = new Vector3(-2, 0, 40f + latency);
            _curBar++;
            yield return new WaitForSecondsRealtime((float)(8 * 60 / _bpm) - instantiateDelay);        //한 마디에 노트 8개 X 1개당 시간 = 60 / 72(bpm) - 인스턴스 딜레이(추정)
        }

        yield return new WaitForSecondsRealtime(6.66f);
        StartCoroutine(VolumeDown());       //스테이지 끝나면 볼륨 다운
    }

    private IEnumerator VolumeDown()
    {
        while (music1.volume > 0)
        {
            music1.volume -= Time.deltaTime / 4;        // 4초에 걸쳐 줄어들도록
            Debug.Log("Volume Down");
            yield return null;
        }

        //TODO : 스테이지 종료 액션 콜백
    }
}

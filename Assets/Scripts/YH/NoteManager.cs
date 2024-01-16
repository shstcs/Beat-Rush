using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteManager : MonoBehaviour
{
    private List<Dictionary<string, object>> _sheet;
    private int _curBar = 1;
    [SerializeField] private SoundManager _soundManager;          //TODO : 나중에 private으로 바꾸고 매니저를 통해 접근할 예정.
    private ObjectPool _notePool;         

    [Range(0f, 2f)]
    public float latency = 0.8f;


    private void Awake()
    {
        _sheet = CSVReader.Read("test");
        _notePool = Managers.Pool;
    }

    private void Start()
    {
        _notePool.SetPool();
        StartCoroutine(CreateNote());
    }

    private IEnumerator CreateNote()
    {
        yield return new WaitForSeconds(latency);
        while (_curBar < 18)
        {
            SetNotes(_curBar);
            _curBar++;
            yield return new WaitForSeconds((float)(8 * 60 / Managers.Game.bpm));        //한 마디에 노트 8개 X 1개당 시간 = 60 / 72(bpm)
        }

        yield return new WaitForSeconds((float)(8 * 60 / Managers.Game.bpm));
        StartCoroutine(_soundManager.VolumeDown());       //스테이지 끝나면 볼륨 다운
    }

    private void SetNotes(int current)
    {
        for (int i = 0; i < _sheet.Count; i++)
        {
            if ((int)_sheet[i]["curBar"] == current)
            {
                GameObject note = _notePool.SpawnFromPool();
                float xPos = (float)_sheet[i]["xValue"];
                float zPos = (float)_sheet[i]["zValue"];
                //note.GetComponentInChildren<SkinnedMeshRenderer>().material.color = Color.black;

                note.transform.position = new Vector3(xPos + 140, 4, zPos + 42.5f);
            }
        }
    }
}

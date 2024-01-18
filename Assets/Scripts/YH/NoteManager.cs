using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteManager : MonoBehaviour
{
    private List<Dictionary<string, object>> _sheet;
    private int _curBar = 1;
    private int _maxNote = 120;

    [SerializeField] private SoundManager _soundManager;          //TODO : Use SoundManager in Managers
    private ObjectPool _notePool;
    private float _noteTime;
    private float _testTime = 0.3f;

    [Range(0f, 2f)]
    public float latency = 0.8f;

    private int _feedbackCount = 0;

    private void Awake()
    {
        _sheet = CSVReader.Read("Sheet1");
    }

    private void Start()
    {
        Managers.Game.GameType = GameType.Play;
        _notePool = Managers.Pool;
        _notePool.SetPool();
        _noteTime = 60 / Managers.Game.bpm;

        _soundManager.PlayClip(5);
        StartCoroutine(CreateNewNotes());
    }

    private void Update()
    {
        if (_feedbackCount > 14)
        {
            //Debug.Log(Time.time + " " + _soundManager.PlayTime());
        }
        _feedbackCount++;
    }

    private IEnumerator CreateNewNotes()
    {
        float waitTime = 0;
        for (int i = 0; i < _sheet.Count; i++)
        {
            waitTime = (float)_sheet[i + 1]["curTime"] - (float)_sheet[i]["curTime"];
            GameObject note = _notePool.SpawnFromPool();
            note.transform.position = new Vector3((float)_sheet[i]["xValue"] + 140, 4, 42.5f);
            yield return new WaitForSeconds(waitTime / 5 * _noteTime);
        }
    }

    private void FeedBack()
    {
        Queue<GameObject> _notes = Managers.Pool.poolQueue;
        foreach (GameObject _note in _notes)
        {
            if (_note.activeSelf)
                Debug.Log(_note.transform.position.z);
        }
    }
}

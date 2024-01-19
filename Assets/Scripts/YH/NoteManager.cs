using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteManager : MonoBehaviour
{
    private List<Dictionary<string, object>> _sheet;

    [SerializeField] private SoundManager _soundManager;          //TODO : Use SoundManager in Managers
    private ObjectPool _notePool;
    private float _noteSpeed;
    private float _startDelay = 0.3f;

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
        _noteSpeed = 5 / (60 / Managers.Game.bpm);

        StartCoroutine(CreateNewNotes());
    }

    private void Update()
    {
        if (_feedbackCount > 12 && _soundManager.PlayTime() > 0)
        {
            //FeedBack();
        }
        _feedbackCount++;
    }

    private IEnumerator CreateNewNotes()
    {
        float waitTime = 0;
        _soundManager.PlayClip(32.5f / _noteSpeed - 0.3f);
        for (int i = 0; i < _sheet.Count - 1; i++)
        {
            waitTime = (float)_sheet[i + 1]["noteLocation"] - (float)_sheet[i]["noteLocation"];
            GameObject note = _notePool.SpawnFromPool();
            note.transform.position = new Vector3((float)_sheet[i]["xValue"] + 140, 4, 42.5f);
            yield return new WaitForSeconds(waitTime / _noteSpeed);
        }
        StartCoroutine(_soundManager.VolumeDown());
    }

    private void FeedBack()
    {
        List<GameObject> _activeNotes = Managers.Pool.GetActiveNotes();
        float _noteDistance = _noteSpeed * (_soundManager.PlayTime() - _startDelay);

        int cnt = 0;
        foreach (Dictionary<string, object> _noteInfo in _sheet)
        {
            float curLocation = (float)_noteInfo["noteLocation"] - _noteDistance;
            if (curLocation >= 0 && curLocation <= 32.5)
            {
                GameObject note = cnt < _activeNotes.Count ? _activeNotes[cnt] : _notePool.SpawnFromPool();
                note.transform.position = new Vector3(note.transform.position.x, note.transform.position.y, curLocation + 10);
                cnt++;

            }
        }
    }
}

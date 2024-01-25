using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class NoteManager : MonoBehaviour
{
    [SerializeField] private GameObject _monsterObject;
    private IMonster _monster;

    private ObjectPool _notePool;
    private float _noteSpeed;
    private double _curDsp;

    private void Awake()
    {
        _monster = _monsterObject.GetComponent<IMonster>();
    }

    private void Start()
    {
        Managers.Game.GameType = GameType.Play;
        _notePool = Managers.Pool;
        _notePool.SetPool();
        _noteSpeed = 5 / (60 / Managers.Game.bpm);
        _curDsp = AudioSettings.dspTime;

        StartCoroutine(CreateNewNotes());
        Managers.Game.OnStageEnd -= Managers.Sound.StopBGM;
        Managers.Game.OnStageEnd += Managers.Sound.StopBGM;
    }

    private void Update()
    {
        if (Time.timeScale > 0)
        {
            MoveNotes();
        }
        _curDsp = AudioSettings.dspTime;
    }

    private void MoveNotes()
    {
        float movement = ((float)(AudioSettings.dspTime - _curDsp) * _noteSpeed);
        foreach (GameObject note in Managers.Pool.GetActiveNotes())
        {
            note.gameObject.transform.position = new Vector3(note.gameObject.transform.position.x, note.gameObject.transform.position.y,
            note.gameObject.transform.position.z - movement);
        }
    }

    private IEnumerator CreateNewNotes()
    {
        Managers.Sound.DelayedPlayBGM(BGM.Stage1,32.5f / _noteSpeed);

        for(int i = 0; i < 4; i++)
        {
            _monster.RandomAttack();
            yield return new WaitForSeconds(33.3333f);
            Managers.Game.curNote = 0;
        }
        _monster.EndStage();
    }
}

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

    [Header("StageData")]
    private int _patternLength;
    private float _attackDelay;
    private BGM _bgm;

    private Animator _cameraAnimator;

    private void Awake()
    {
        _monster = _monsterObject.GetComponent<IMonster>();
        _cameraAnimator = Camera.main.GetComponent<Animator>();
    }

    private void Start()
    {
        Managers.Game.GameType = GameType.Play;
        _notePool = Managers.Pool;
        _notePool.SetPool();
        _noteSpeed = Managers.Game.noteDistance[Managers.Game.currentStage] / (60 / Managers.Game.bpm[Managers.Game.currentStage]);
        _curDsp = AudioSettings.dspTime;

        StartCoroutine(CreateNewNotes());
        Managers.Game.OnStageEnd += Managers.Sound.StopBGM;
        Managers.Game.OnStageEnd += ClearStageUpdate;
        Managers.Game.OnStageEnd += Managers.Game.InitNotes;
        
    }

    private void Update()
    {
        if (Time.timeScale > 0)
        {
            MoveNotes();
        }
        _curDsp = AudioSettings.dspTime;
    }

    private void OnDestroy()
    {
        Managers.Game.OnStageEnd -= Managers.Sound.StopBGM;
        Managers.Game.OnStageEnd -= Managers.Game.InitNotes;
        Managers.Game.OnStageEnd -= ClearStageUpdate;
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
        var data = _monster.GetPatternData();
        _patternLength = data.length;
        _attackDelay = data.delay;
        _bgm = data.bgm;
        Managers.Sound.DelayedPlayBGM(_bgm, (32.5f / _noteSpeed));

        for (int i = 0; i < _patternLength; i++)
        {
            _monster.RandomAttack();
            yield return new WaitForSeconds(_attackDelay);
            Managers.Game.curNote = 0;
            if (i != _patternLength - 1) _cameraAnimator.SetTrigger("Move");
        }
        _cameraAnimator.SetTrigger("EndMove");
        yield return new WaitForSeconds(1f);
        _monster.EndStage();
    }

    private void ClearStageUpdate()
    {
        Managers.Data.CurrentStateData.CurrentClearStage = Managers.Data.CurrentStateData.CurrentClearStage < Managers.Game.currentStage + 1 ?
            Managers.Game.currentStage + 1 : Managers.Data.CurrentStateData.CurrentClearStage;

        // 데이터 저장
        var currentStageData = Managers.Game.MaxScoreArray[Managers.Game.currentStage];
        currentStageData.SetData(Managers.Game.Score);

        // 퀘스트 완료
        QuestManager.instance.SetQuestClear(QuestName.StageFirstComplete);
        if (Managers.Data.CurrentStateData.GetHealth() == Managers.Data.CurrentStateData.CurrentHealth)
            QuestManager.instance.SetQuestClear(QuestName.MaxHealthClear);
    }
}

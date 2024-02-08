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
    private float _stageNoteSpeed;
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
        Managers.Game.GameType = GameType.Play;
    }

    private void Start()
    {
        _notePool = Managers.Pool;
        _notePool.SetPool();
        _stageNoteSpeed = Managers.Game.noteSpeed[Managers.Game.currentStage] * Managers.Game.speedModifier;
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
        float movement = ((float)(AudioSettings.dspTime - _curDsp) * _stageNoteSpeed);
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
        Managers.Sound.DelayedPlayBGM(_bgm, (32.5f / _stageNoteSpeed));

        for (int i = 0; i < _patternLength; i++)
        {
            _monster.RandomAttack();
            yield return new WaitForSeconds(_attackDelay);
            if (i != _patternLength - 1) _cameraAnimator.SetTrigger("Move");
        }
        _cameraAnimator.SetTrigger("EndMove");
        yield return new WaitForSeconds((32.5f / _stageNoteSpeed));
        _monster.EndStage();
    }

    private void ClearStageUpdate()
    {
        Managers.Data.CurrentStateData.CurrentClearStage = Managers.Data.CurrentStateData.CurrentClearStage < Managers.Game.currentStage + 1 ?
            Managers.Game.currentStage + 1 : Managers.Data.CurrentStateData.CurrentClearStage;

        // 데이터 저장
        if (!Managers.Player.IsDie())
        {
            var currentStageData = Managers.Game.MaxScoreArray[Managers.Game.currentStage];
            currentStageData.SetData(Managers.Game.Score);
        }

        // 퀘스트 완료
        QuestManager.instance.SetQuestClear(QuestName.StageFirstComplete);
        if (Managers.Data.CurrentStateData.GetHealth() == Managers.Data.CurrentStateData.CurrentHealth)
            QuestManager.instance.SetQuestClear(QuestName.MaxHealthClear);

        Managers.Sound.PlaySFX(SFX.GameClear, 0.5f);
    }
}

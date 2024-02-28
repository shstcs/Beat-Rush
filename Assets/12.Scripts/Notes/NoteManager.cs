using System;
using System.Collections;
using UnityEngine;

public class NoteManager : MonoBehaviour
{
    [SerializeField] private GameObject _monsterObject;
    private Monster _monster;

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
        _monster = _monsterObject.GetComponent<Monster>();
        _cameraAnimator = Camera.main.GetComponent<Animator>();
        Managers.Game.GameType = GameType.Play;
        Managers.Game.OnStageStart += StageStart;
    }

    public void StageStart()
    {
        StartCoroutine(CreateNewNotes());
    }

    private void Start()
    {
        _notePool = Managers.Pool;
        _notePool.SetPool();
        _stageNoteSpeed = Managers.Game.stageInfos[Managers.Game.currentStage].noteSpeed * Managers.Game.speedModifier;
        _curDsp = AudioSettings.dspTime;
        if (Managers.Game.currentStage == 0) StartCoroutine(CreateNewNotes());

        Managers.Game.OnStageEnd += Managers.Sound.StopBGM;
        Managers.Game.OnStageEnd += ClearStageUpdate;
        Managers.Game.OnStageEnd += Managers.Game.InitNotes;
        Managers.Game.OnContinue += Managers.Game.currentStage == 0 ? Managers.Sound.ContinueBGM : DelayContinued;
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
        Managers.Game.OnContinue -= Managers.Game.currentStage == 0 ? Managers.Sound.ContinueBGM : DelayContinued;
        Managers.Game.OnStageStart -= StageStart;
    }

    private void MoveNotes()
    {
        float movement = ((float)(AudioSettings.dspTime - _curDsp) * _stageNoteSpeed);
        foreach (GameObject note in Managers.Pool.GetActiveNotes())
        {
            note.transform.position = new Vector3(note.transform.position.x, note.transform.position.y,
            note.transform.position.z - movement);
        }
    }

    public IEnumerator CreateNewNotes()
    {
        Managers.Sound.StopBGM();

        (_patternLength, _attackDelay, _bgm) = _monster.GetPatternData();

        if (Managers.Game.currentStage != 0)
        {
            Managers.Sound.DelayedPlayBGM(_bgm, (32.5f / _stageNoteSpeed));
        }

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

    private void DelayContinued()
    {
        StartCoroutine(Managers.Popup.DelayContinue());
    }

    private void ClearStageUpdate()
    {
        // 데이터 저장
        if (!Managers.Player.IsDie())
        {
            Managers.Data.CurrentStateData.CurrentClearStage = Managers.Data.CurrentStateData.CurrentClearStage < Managers.Game.currentStage + 1 ?
                Managers.Game.currentStage + 1 : Managers.Data.CurrentStateData.CurrentClearStage;
            var currentStageData = Managers.Game.MaxScoreArray[Managers.Game.currentStage];
            currentStageData.SetData(Managers.Game.Score);
            Managers.Data.SavePlayerData();
            // 퀘스트 완료
            if (Managers.Game.currentStage != 0)
            {
                QuestManager.instance.SetQuestClear(QuestName.StageFirstComplete);
                if (Managers.Data.CurrentStateData.GetHealth() == Managers.Data.CurrentStateData.CurrentHealth)
                    QuestManager.instance.SetQuestClear(QuestName.MaxHealthClear);
                if (Managers.Game.speedModifier >= 1.5f)
                    QuestManager.instance.SetQuestClear(QuestName.SpeedUpClear);
                if (Managers.Game.mode == GameMode.Sudden)
                    QuestManager.instance.SetQuestClear(QuestName.SuddenModeClear);
            }
        }

        Managers.Sound.PlaySFX(SFX.GameClear, 0.5f);
    }
}

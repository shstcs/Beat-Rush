using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    #region UnityAction
    [Header("Events")]
    public UnityAction OnGameStart;
    public UnityAction OnGameOver;
    public UnityAction OnMapStart;
    public UnityAction OnStageStart;
    public UnityAction OnStageEnd;
    public UnityAction GetKeyDown;
    public UnityAction OnCombo;
    public UnityAction OnLevel;
    public UnityAction OnDamaged;
    public UnityAction OnContinue;
    public UnityAction OnCheckNote;
    #endregion
    #region Fields
    public Vector3 PlayerSpwanPosition = new Vector3(43f, 0f, 14f);
    public Quaternion PlayerSpwanRotation = Quaternion.Euler(0f, 0f, 0f);
    public float CinemachinemVerticalAxisValue = 0f;
    public float CinemachinemmHorizontalAxisValue = 0f;

    public int[] judgeNotes = new int[5];
    public string curJudge;
    public int Combo { get; set; }
    public int MaxCombo { get; set; }
    public StageData[] MaxScoreArray;

    public int Score { get; set; }
    public int Hp { get; private set; }
    public StageInfo[] stageInfos =
    {
       new Stage0Info(),
       new Stage1Info(),
       new Stage2Info(),
       new Stage3Info()
    };
    public int currentStage = 0;
    public float delay = 1.5f;
    public GameType GameType = GameType.Lobby;
    public InputLockType lockType = InputLockType.UnLock;
    public Rank rank = Rank.S;

    [Header("GameMode")]
    public GameMode mode = GameMode.normal;
    public float speedModifier = 1.0f;

    public Dictionary<QuestName, QuestData> questDatas = new Dictionary<QuestName, QuestData>();
    //private int bestScore;
    #endregion
    #region Methods
    protected virtual void OnEnable()
    {
        CallGameStart();
    }
    public void CallGameStart()
    {
        OnGameStart?.Invoke();
    }
    public void CallStageStart()
    {
        OnStageStart?.Invoke();
    }
    public void CallStageEnd()
    {
        OnStageEnd?.Invoke();
    }
    public void CallCombo()
    {
        OnCombo?.Invoke();
    }
    public void CallLevel()
    {
        OnLevel?.Invoke();
    }
    public void CallDamaged()
    {
        OnDamaged?.Invoke();
    }
    public void CallContinue()
    {
        OnContinue?.Invoke();
    }
    public void CallCheckNote()
    {
        OnCheckNote?.Invoke();
    }
    public void AddScore(int score)
    {
        Score += score;
    }

    public void InitNotes()
    {
        foreach (StageInfo info in stageInfos)
        {
            for (int i = 0; i < info.curNoteInStage.Length; i++)
            {
                info.curNoteInStage[i] = 0;
            }
        }
    }
    public void InitJudge()
    {
        for (int i = 0; i < judgeNotes.Length; i++)
        {
            judgeNotes[i] = 0;
        }
    }

    public void InitMaxScoreArray()
    {
        var stageCount = Enum.GetValues(typeof(InstrumentType)).Length;
        MaxScoreArray = new StageData[stageCount];
        for (int i = 0; i < stageCount; i++)
        {
            MaxScoreArray[i] = new StageData();
        }
    }
    public void SetRank()
    {
        int noteCount = judgeNotes[0] + judgeNotes[1] + judgeNotes[2] + judgeNotes[3] + judgeNotes[4];
        if (noteCount == 0)
            noteCount = 1;
        float rankCount = (float)(judgeNotes[0] + judgeNotes[1]) / noteCount;
        if (Managers.Player.IsDie())
            rank = Rank.F;
        else
        {
            if (rankCount >= 0.9f && judgeNotes[3] == 0 && judgeNotes[4] == 0)
                rank = Rank.S;
            else if (rankCount >= 0.9f && judgeNotes[3] > 0 && judgeNotes[4] > 0)
                rank = Rank.A;
            else if (rankCount >= 0.6f && rankCount < 0.9f)
                rank = Rank.B;
            else
                rank = Rank.C;
        }
    }
    private void OnApplicationQuit()
    {
        Managers.Data.SaveData();
    }

    #endregion
}
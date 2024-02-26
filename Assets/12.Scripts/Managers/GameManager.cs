using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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
    public float[] bpm = { 80f, 72f, 99f, 100f };
    public float[] noteDistance = { 5, 8, 8, 8 };
    public float[] noteSpeed = { 6.6666f, 9.6f, 13.2f, 13.3333f };
    public int[,] curNoteInStage = new int[4, 20];
    public float[] StageStartDelay = { 0, 0, -2.5f, -4f };
    public Vector3[] StageNotePos =
    {
        new Vector3(-2, 0, 42.5f),
        new Vector3(40, 1, 42.5f),
        new Vector3(40, 1, 42.5f),
        new Vector3(40, 1, 42.5f)
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
    public void AddScore(int score)
    {
        Score += score;
    }

    public void InitNotes()
    {
        for (int i = 0; i < curNoteInStage.GetLength(0); i++)
        {
            for (int j = 0; j < curNoteInStage.GetLength(1); j++)
            {
                curNoteInStage[i, j] = 0;
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
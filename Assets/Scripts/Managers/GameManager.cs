using System;
using System.Collections;
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
    #endregion
    #region Fields
    private int _lobbyPopupCount = 0;

    public int LobbyPopupCount
    {
        get
        {
            return _lobbyPopupCount;
        }
        set
        {
            _lobbyPopupCount = value;
            if (_lobbyPopupCount == 0)
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                Time.timeScale = 1.0f;
                Managers.Game.lockType = InputLockType.UnLock;
            }
            else
            {
                Managers.Game.lockType = InputLockType.Lock;
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
                Time.timeScale = 0.0f;
            }
        }
    }

    public int[] judgeNotes = new int[5];
    public string curJudge;
    public int Combo { get; set; }
    public int MaxCombo { get; set; }
    public StageData[] MaxScoreArray;

    public int Score { get; set; }
    public int Hp { get; private set; }
    public float[] bpm = { 80f, 72f, 99f };
    public float[] noteDistance = { 5, 5, 8 };
    public int[,] curNoteInStage = new int[4, 15];
    public float[] StageStartDelay = { 0, 0.5f, -1.5f, 0f };
    public Vector3[] StageNotePos =
    {
        new Vector3(-2, 0, 42.5f),
        new Vector3(40, 1, 42.5f),
        new Vector3(40, 1, 42.5f),
        new Vector3(40, 1, 42.5f)
    };
    public int currentStage = 1;
    public float delay = 2.9f;
    public int curNote = 0;
    public GameType GameType = GameType.Main;
    public InputLockType lockType = InputLockType.UnLock;

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


    #endregion
}
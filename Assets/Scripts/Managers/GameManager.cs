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
    public int[] judgeNotes = new int[5];
    public string curJudge;
    public int Combo { get; set; }
    public int MaxCombo { get; set; }
    public int Score { get; set; }
    public int Hp { get; private set; }
    public float[] bpm = { 80f, 72f, 99f };
    public float[] noteDistance = { 5, 5, 8 };
    public int[] curNoteInStage2 = new int[10];
    public float[] StageStartDelay = {0, 0.5f, -1.5f, 0f };
    public int currentStage = 1;
    public float delay = 2.1f;
    public int curNote = 0;
    public GameType GameType = GameType.Main;
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
        for (int i = 0; i < judgeNotes.Length; i++)
        {
            judgeNotes[i] = 0;
        }
        for(int i = 0; i<curNoteInStage2.Length; i++)
        {
            curNoteInStage2[i] = 0;
        }
    }
    #endregion
}
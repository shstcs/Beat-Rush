using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    #region UnityAction
    [Header ("Events")]
    public UnityAction OnGameStart;
    public UnityAction OnGameOver;
    public UnityAction OnMapStart;
    public UnityAction OnStageStart;
    public UnityAction OnStageEnd;
    #endregion
    #region Fields
    public int Combo { get; set; }
    public int Score { get; private set; }
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
    #endregion
}
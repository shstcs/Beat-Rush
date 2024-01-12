using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : Managers
{
    private void Awake()
    {
        Init();
    }
    #region Methods
    public void CallStageStart()
    {
        OnStageStart?.Invoke();
    }
    public void CallStageEnd()
    {
        OnStageEnd?.Invoke();
    }
    public void CallKeyGet()
    {
        OnKeyGet?.Invoke();
    }
    #endregion
}
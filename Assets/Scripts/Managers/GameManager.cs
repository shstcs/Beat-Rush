using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    #region UnityAction
    public UnityAction OnStageStart;
    public UnityAction OnStageEnd;
    public UnityAction OnKeyGet;
    #endregion
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
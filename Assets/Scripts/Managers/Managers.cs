using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Managers : MonoBehaviour
{
    #region Singleton

    private static Managers _instance;
    private static bool _initialized;

    public static Managers Instance 
    { 
        get
        {
            if(!_initialized)
            {
                _initialized = true;
                Init();
            }
            return _instance;
        }
    }
    protected static void Init()
    {
        if(_instance == null)
        {
            _instance = (Managers)FindObjectOfType(typeof(Managers));

            if(_instance == null )
            {
                GameObject gameObject = new GameObject { name = "@Managers" };
                if(gameObject.GetComponent<Managers>() == null)
                {
                    _instance = gameObject.AddComponent<Managers>();
                }
                DontDestroyOnLoad(gameObject);
            }
        }
    }
    #endregion
    #region UnityAction
    public UnityAction OnStageStart;
    public UnityAction OnStageEnd;
    public UnityAction OnKeyGet;
    #endregion
}

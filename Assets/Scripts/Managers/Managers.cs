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
            if (!_initialized)
            {
                _initialized = true;
                Init();
            }
            return _instance;
        }
    }
    protected static void Init()
    {
        if (_instance == null)
        {
            _instance = (Managers)FindObjectOfType(typeof(Managers));

            if (_instance == null)
            {
                GameObject gameObject = new GameObject { name = "@Managers" };
                if (gameObject.GetComponent<Managers>() == null)
                {
                    _instance = gameObject.AddComponent<Managers>();
                }
                DontDestroyOnLoad(gameObject);
            }
        }
    }
    #endregion
    #region Fields
    private UIManager _ui = new();
    private GameManager _game = new();
    private ResourceManager _resource = new();
    private ObjectPool _pool = new();

    public static UIManager UI => Instance?._ui;
    public static GameManager Game => Instance?._game;
    public static ResourceManager Resource => Instance?._resource;
    public static ObjectPool Pool
    {
        get { return Instance._pool; }
        set { Instance._pool = value; }
    }
    #endregion
}

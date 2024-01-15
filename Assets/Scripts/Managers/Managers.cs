using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

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
    private static void Init()
    {
        if (_instance == null)
        {
            GameObject gameObject = GameObject.Find("@Managers");

            if (gameObject == null)
            {
                gameObject = new GameObject { name = "@Managers" };
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
    private ResourceManager _resource = new();
    private GameManager _game = new();
    private UIManager _ui = new();

    public static UIManager UI => Instance?._ui;
    public static GameManager Game => Instance?._game;
    public static ResourceManager Resource => Instance?._resource;
    #endregion
}

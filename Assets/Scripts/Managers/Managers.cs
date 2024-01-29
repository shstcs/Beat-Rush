using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Managers : MonoBehaviour
{
    #region Singleton

    private static Managers _instance;
    public static Managers Instance
    {
        get
        {
            if (!_instance)
            {
                _instance = GameObject.FindObjectOfType<Managers>();
                Init();
            }
            return _instance;
        }
    }
    protected static void Init()
    {
        if (!_instance)
        {
            GameObject gameObject = new GameObject { name = "@Managers" };
            if(gameObject.GetComponent<Managers>() == null )
            {
                _instance = gameObject.AddComponent<Managers>();
            }
            Sound.AudioSourceBGM = gameObject.AddComponent<AudioSource>();
            Sound.AudioSourceSFX = gameObject.AddComponent<AudioSource>();
            DontDestroyOnLoad(gameObject);
        }
    }
    #endregion
    #region Fields
    private UIManager _ui = new();
    private GameManager _game = new();
    private ResourceManager _resource = new();
    private ObjectPool _pool = new();
    private Player _player = new();
    private SoundManager _sound = new();

    public static UIManager UI => Instance?._ui;
    public static GameManager Game => Instance?._game;
    public static ResourceManager Resource => Instance?._resource;
    public static SoundManager Sound => Instance?._sound;
    public static ObjectPool Pool
    {
        get { return Instance._pool; }
        set { Instance._pool = value; }
    }
    public static Player Player
    {
        get { return Instance._player; }
        set { Instance._player = value; }
    }
    #endregion

    /*
    #region Test
    [SerializeField] public GameObject ClearPanel;
    [SerializeField] public TextMeshProUGUI ClearScore;
    [SerializeField] public TextMeshProUGUI ScoreText;
    [SerializeField] public TextMeshProUGUI ComboText;
    [SerializeField] public Image HpBar;
    [SerializeField] public Image SkillBar;
    private float maxskill;
    public float currentHealth;
    public float currentskill;
    private void Start()
    {
        maxskill = 100.0f;
    }
    private void Update()
    {
        ScoreText.text = Game.Score.ToString();
        ComboText.text = Game.Combo.ToString();
        HpBar.fillAmount = (float)Player.CurrentStateData.Health / Player.Data.StateData.Health;
        SkillBar.fillAmount = (float)Player.CurrentStateData.SkillGauge / maxskill;
    }
    public void SetClearPanel()
    {
        ClearPanel.SetActive(true);
        ClearScore.text = Game.Score.ToString();
        //Time.timeScale = 0f;
    }
    #endregion
    */
}

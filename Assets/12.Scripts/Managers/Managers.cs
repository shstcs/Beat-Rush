using UnityEngine;

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
            if (gameObject.GetComponent<Managers>() == null)
            {
                _instance = gameObject.AddComponent<Managers>();
            }
            Sound.AudioSourceBGM = gameObject.AddComponent<AudioSource>();
            Sound.AudioSourceSFX = gameObject.AddComponent<AudioSource>();
            DontDestroyOnLoad(gameObject);
            Game.InitMaxScoreArray();
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
    private DataManager _data = new();
    private PopupManager _popup = new();

    public static UIManager UI => Instance?._ui;
    public static GameManager Game => Instance?._game;
    public static ResourceManager Resource => Instance?._resource;
    public static SoundManager Sound => Instance?._sound;
    public static DataManager Data => Instance?._data;
    public static PopupManager Popup => Instance?._popup;
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
}
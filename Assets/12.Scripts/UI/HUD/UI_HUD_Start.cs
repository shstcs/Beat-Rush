using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class UI_HUD_Start : MonoBehaviour
{
    #region fields
    private GameObject _option;
    private GameObject _credit;
    #endregion
    #region MonoBehaviour
    private void Start()
    {
        _option = GameObject.Find("Canvas").transform.GetChild(2).gameObject;
        _credit = GameObject.Find("Canvas").transform.GetChild(3).gameObject;
    }
    #endregion
    #region methods
    public void NewGame()
    {
        Managers.Data.DeleteAllFile();
        Managers.Game.GameType = GameType.Play;
        SceneManager.LoadScene("Tutorial");
    }

    public void Continue()
    {
        if (!Managers.Data.LoadFileCheck("PlayerSave"))
        {
            Debug.Log("Load File Not Exist!!!");
        }
        else
        {
            Managers.Data.LoadData();
            SceneManager.LoadScene("Lobby");
        }
    }
    public void Option()
    {
        _credit.SetActive(false);
        if (_option.activeSelf == true)
            _option.SetActive(false);
        else if (_option.activeSelf == false)
            _option.SetActive(true);
    }
    public void Credit()
    {
        _option.SetActive(false);
        if (_credit.activeSelf == true)
            _credit.SetActive(false);
        else if (_credit.activeSelf == false)
            _credit.SetActive(true);
    }
    public void ExitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Apllication.Quit();
#endif
    }

    #endregion

}

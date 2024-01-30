using System;
using System.Collections;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerData
{
    public int Level;
    public int Exp;
    public int CurrentClearStage;
    public int BestScore;
}

public class DataManager : MonoBehaviour
{
    string path;
    string filename = "save";

    [HideInInspector] public int BestScore;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    public void SaveData()
    {
        PlayerData playerData = new PlayerData();

        playerData.Level = Managers.Player.CurrentStateData.Level;
        playerData.Exp = Managers.Player.CurrentStateData.Exp;
        playerData.CurrentClearStage = Managers.Player.CurrentStateData.CurrentClearStage;

        playerData.BestScore = PlayerPrefs.GetInt("BestScore");

        path = Application.persistentDataPath + "/";
        string data = JsonUtility.ToJson(playerData, true);
        File.WriteAllText(path + filename, data);
        Debug.Log(data);
        Debug.Log("SavePath : " + path);
    }

    public void LoadData()
    {
        path = Application.persistentDataPath + "/";
        Debug.Log("Load");
        string data = File.ReadAllText(path + filename);
        PlayerData playerData = JsonUtility.FromJson<PlayerData>(data);

        Managers.Player.CurrentStateData.Level = playerData.Level;
        Managers.Player.CurrentStateData.Exp = playerData.Exp;
        Managers.Player.CurrentStateData.CurrentClearStage = playerData.CurrentClearStage;

        Managers.Data.BestScore = playerData.BestScore;
    }

    public bool LoadFileCheck()
    {
        path = Application.persistentDataPath + "/" + filename;
        return File.Exists(path);
    }
}
using System;
using System.Collections;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerData
{
    public int Level;
    public int Exp;
    public int BestScore;
}

public class DataManager : MonoBehaviour
{
    string path;
    string filename = "save";

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    public void SaveData()
    {
        PlayerData playerData = new PlayerData();

        Managers.Player.CurrentStateData.Level++; // Test
        playerData.Level = Managers.Player.CurrentStateData.Level;
        path = Application.persistentDataPath + "/";
        string data = JsonUtility.ToJson(playerData, true);
        File.WriteAllText(path + filename, data);
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
    }

    public bool LoadFileCheck()
    {
        path = Application.persistentDataPath + "/" + filename;
        return File.Exists(path);
    }
}
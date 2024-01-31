using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using Newtonsoft.Json;
using static Cinemachine.DocumentationSortingAttribute;

public class PlayerData
{
    //public int Level;
    //public int Exp;
    //public int CurrentClearStage;
    public int BestScore;

    public PlayerStateData StateData;
    public PlayerSkillData SkillData;
}

public class DataManager : MonoBehaviour
{
    string path;
    string playerDataFileName = "save";
    string questDataFileName = "QuestSave";

    private PlayerSO baseData;

    [HideInInspector] public int BestScore;
    public int testAA = 1;
    [HideInInspector]
    public PlayerStateData CurrentStateData = new();
    [HideInInspector]
    public PlayerSkillData CurrentSkillData = new();

    [HideInInspector]
    public Dictionary<QuestName, QuestData> questData = new Dictionary<QuestName, QuestData>();

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    public void SaveData()
    {
        PlayerData playerData = new PlayerData();

        playerData.StateData = CurrentStateData;
        playerData.SkillData = CurrentSkillData;

        //playerData.Level = Managers.Player.CurrentStateData.Level;
        //playerData.Exp = Managers.Player.CurrentStateData.Exp;
        //playerData.CurrentClearStage = Managers.Player.CurrentStateData.CurrentClearStage;

        playerData.BestScore = PlayerPrefs.GetInt("BestScore");

        path = Application.persistentDataPath + "/";
        string data = JsonUtility.ToJson(playerData, true);

        questData = Managers.Game.questDatas;

        var questSaveData = JsonConvert.SerializeObject(questData, Formatting.Indented);

        File.WriteAllText(path + playerDataFileName, data);
        File.WriteAllText(path + questDataFileName, questSaveData);

        Debug.Log(data);
        Debug.Log(questSaveData);
        Debug.Log("SavePath : " + path);
    }

    public void LoadData()
    {
        if (!LoadFileCheck())
        {
            InitData();
        }
        else
        {
            path = Application.persistentDataPath + "/";
            Debug.Log("Load");
            string data = File.ReadAllText(path + playerDataFileName);
            PlayerData playerData = JsonUtility.FromJson<PlayerData>(data);

            CurrentStateData = playerData.StateData;
            CurrentSkillData = playerData.SkillData;

            Managers.Data.BestScore = playerData.BestScore;
        }

        if (!LoadQuestFileCheck()) return;

        string questSaveData = File.ReadAllText(path);
        Managers.Game.questDatas = JsonConvert.DeserializeObject<Dictionary<QuestName, QuestData>>(questSaveData);


        //Managers.Player.CurrentStateData.Level = playerData.Level;
        //Managers.Player.CurrentStateData.Exp = playerData.Exp;
        //Managers.Player.CurrentStateData.CurrentClearStage = playerData.CurrentClearStage;

    }

    public bool LoadFileCheck()
    {
        path = Application.persistentDataPath + "/" + playerDataFileName;
        return File.Exists(path);
    }

    private bool LoadQuestFileCheck()
    {
        path = Application.persistentDataPath + "/" + questDataFileName;
        return File.Exists(path);
    }

    private void InitData()
    {
        baseData = Managers.Resource.Load<PlayerSO>("PlayerSO");
        CurrentStateData.DeepCopy(baseData.StateData);
        CurrentSkillData.DeepCopy(baseData.SkillData);
    }
}
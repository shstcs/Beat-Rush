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
    public float delay;

    public PlayerStateData StateData;
    public PlayerSkillData SkillData;
}

public class SoundData
{
    public float MasterVolume = 1f;
    public float VolumeSFX = 1f;
    public float VolumeBGM = 1f;
}

public class DataManager : MonoBehaviour
{
    string path;
    string playerDataFileName = "PlayerSave";
    string questDataFileName = "QuestSave";
    string soundDataFileName = "SoundSave";
    string stageDataFileName = "StageSave";

    private PlayerSO baseData;

    [HideInInspector] public int BestScore;
    [HideInInspector]
    public PlayerStateData CurrentStateData = new();
    [HideInInspector]
    public PlayerSkillData CurrentSkillData = new();
    [HideInInspector]
    public Dictionary<QuestName, QuestData> questData = new Dictionary<QuestName, QuestData>();
    [HideInInspector]
    public SoundData soundData;
    [HideInInspector]
    public StageData[] stageData = new StageData[Enum.GetValues(typeof(InstrumentType)).Length];

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    public void SaveData()
    {
        SavePlayerData();
        SaveQuestData();
        SaveSoundData();
        SaveStageData();
    }

    public void LoadData()
    {
        LoadPlayerData();
        LoadQuestData();
        LoadSoundData();
        LoadStageData();

        //Managers.Player.CurrentStateData.Level = playerData.Level;
        //Managers.Player.CurrentStateData.Exp = playerData.Exp;
        //Managers.Player.CurrentStateData.CurrentClearStage = playerData.CurrentClearStage;

    }

    public bool LoadFileCheck(string dataFileName)
    {
        path = Application.persistentDataPath + "/" + dataFileName;
        return File.Exists(path);
    }

    public void DeleteAllFile()
    {
        path = Application.persistentDataPath;
        if (Directory.Exists(path))
        {
            Directory.Delete(path, true);
        }
    }

    #region Load
    private void LoadPlayerData()
    {
        if (!LoadFileCheck(playerDataFileName))
        {
            baseData = Managers.Resource.Load<PlayerSO>("PlayerSO");
            CurrentStateData.DeepCopy(baseData.StateData);
            CurrentSkillData.DeepCopy(baseData.SkillData);
            return;
        }

        path = Application.persistentDataPath + "/";
        Debug.Log("Load");
        string data = File.ReadAllText(path + playerDataFileName);
        PlayerData playerData = JsonUtility.FromJson<PlayerData>(data);

        CurrentStateData = playerData.StateData;
        CurrentSkillData = playerData.SkillData;

        Managers.Data.BestScore = playerData.BestScore;
        Managers.Game.delay = playerData.delay;
    }

    private void LoadQuestData()
    {
        if (!LoadFileCheck(questDataFileName)) return;

        string questSaveData = File.ReadAllText(path);
        Managers.Game.questDatas = JsonConvert.DeserializeObject<Dictionary<QuestName, QuestData>>(questSaveData);
    }

    private void LoadSoundData()
    {
        if (!LoadFileCheck(soundDataFileName))
        {
            return;
        }
        else
        {
            string data = File.ReadAllText(path);
            soundData = JsonUtility.FromJson<SoundData>(data);
        }

        Managers.Sound.MasterVolume = soundData.MasterVolume;
        Managers.Sound.SFXVolume = soundData.VolumeSFX;
        Managers.Sound.BGMVolume = soundData.VolumeBGM;
    }

    private void LoadStageData()
    {
        if (!LoadFileCheck(stageDataFileName))
        {
            return;
        }
        else
        {
            string data = File.ReadAllText(path);
            stageData = JsonConvert.DeserializeObject<StageData[]>(data);

            Managers.Game.MaxScoreArray = stageData;
        }
    }

    #endregion

    #region Save
    private void SavePlayerData()
    {
        PlayerData playerData = new PlayerData();

        playerData.StateData = CurrentStateData;
        playerData.SkillData = CurrentSkillData;

        //playerData.Level = Managers.Player.CurrentStateData.Level;
        //playerData.Exp = Managers.Player.CurrentStateData.Exp;
        //playerData.CurrentClearStage = Managers.Player.CurrentStateData.CurrentClearStage;

        playerData.BestScore = PlayerPrefs.GetInt("BestScore");
        playerData.delay = Managers.Game.delay;

        path = Application.persistentDataPath + "/";
        string data = JsonUtility.ToJson(playerData, true);

        File.WriteAllText(path + playerDataFileName, data);
        Debug.Log(data);
        Debug.Log("SavePath : " + path);
    }

    private void SaveQuestData()
    {
        questData = Managers.Game.questDatas;

        var questSaveData = JsonConvert.SerializeObject(questData, Formatting.Indented);

        File.WriteAllText(path + questDataFileName, questSaveData);

        Debug.Log(questSaveData);
    }

    private void SaveSoundData()
    {
        SoundData soundData = new SoundData();

        soundData.MasterVolume = Managers.Sound.MasterVolume;
        soundData.VolumeSFX = Managers.Sound.SFXVolume;
        soundData.VolumeBGM = Managers.Sound.BGMVolume;

        string soundSaveData = JsonUtility.ToJson(soundData, true);

        File.WriteAllText(path + soundDataFileName, soundSaveData);
        Debug.Log(soundSaveData);
    }

    private void SaveStageData()
    {
        stageData = Managers.Game.MaxScoreArray;

        string stageSaveData = JsonConvert.SerializeObject(stageData);
        File.WriteAllText(path + stageDataFileName, stageSaveData);
        Debug.Log(stageSaveData);
    }

    #endregion
}
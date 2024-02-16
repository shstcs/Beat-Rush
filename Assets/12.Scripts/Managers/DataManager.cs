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
    }

    public bool LoadFileCheck(string dataFileName = "")
    {
        path = Application.persistentDataPath + "/" + dataFileName;
        return File.Exists(path);
    }

    public bool LoadDirCheck()
    {
        return Directory.Exists(Application.persistentDataPath);
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

        Debug.Log("Load");
        string data = File.ReadAllText(path);
        PlayerData playerData = JsonUtility.FromJson<PlayerData>(data);

        CurrentStateData = playerData.StateData;
        CurrentSkillData = playerData.SkillData;

        Managers.Game.delay = playerData.delay;
    }

    private void LoadQuestData()
    {
        if (!LoadFileCheck(questDataFileName)) return;

        string questSaveData = File.ReadAllText(path);
        Managers.Game.questDatas = JsonConvert.DeserializeObject<Dictionary<QuestName, QuestData>>(questSaveData);
    }

    public void LoadSoundData()
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
    public void SavePlayerData()
    {
        PlayerData playerData = new PlayerData();

        playerData.StateData = CurrentStateData;
        playerData.SkillData = CurrentSkillData;
        playerData.delay = Managers.Game.delay;

        string data = JsonUtility.ToJson(playerData, true);

        DataSave(data, playerDataFileName);

        Debug.Log(data);
        Debug.Log("SavePath : " + path);
    }

    public void SaveQuestData()
    {
        questData = Managers.Game.questDatas;

        var questSaveData = JsonConvert.SerializeObject(questData, Formatting.Indented);

        DataSave(questSaveData, questDataFileName);

        Debug.Log(questSaveData);
    }

    public void SaveSoundData()
    {
        SoundData soundData = new SoundData();

        soundData.MasterVolume = Managers.Sound.MasterVolume;
        soundData.VolumeSFX = Managers.Sound.SFXVolume;
        soundData.VolumeBGM = Managers.Sound.BGMVolume;

        string soundSaveData = JsonUtility.ToJson(soundData, true);

        DataSave(soundSaveData, soundDataFileName);

        Debug.Log(soundSaveData);
    }

    public void SaveStageData()
    {
        stageData = Managers.Game.MaxScoreArray;

        string stageSaveData = JsonConvert.SerializeObject(stageData);

        DataSave(stageSaveData, stageDataFileName);

        Debug.Log(stageSaveData);
    }

    private void DataSave(string dataStr, string fileName)
    {
        path = Application.persistentDataPath + "/";
        File.WriteAllText(path + fileName, dataStr);
        Debug.Log(path + fileName);
    }

    #endregion
}
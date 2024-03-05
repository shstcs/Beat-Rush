using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

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
    string playerDataFileName = "PlayerSaveData";
    string questDataFileName = "QuestSaveData";
    string soundDataFileName = "SoundSaveData";
    string stageDataFileName = "StageSaveData";

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
        path = Application.persistentDataPath + "/";
        if (File.Exists(path + "PlayerSave"))
        {
            File.Delete(path + "PlayerSave");
        }

        if (File.Exists(path + "QuestSave"))
        {
            File.Delete(path + "QuestSave");
        }

        if (File.Exists(path + "StageSave"))
        {
            File.Delete(path + "StageSave");
        }

        if (File.Exists(path + "SoundSave"))
        {
            File.Delete(path + "SoundSave");
        }


        if (File.Exists(path + "PlayerSaveData"))
        {
            File.Delete(path + "PlayerSaveData");
        }

        if (File.Exists(path + "QuestSaveData"))
        {
            File.Delete(path + "QuestSaveData");
        }

        if (File.Exists(path + "StageSaveData"))
        {
            File.Delete(path + "StageSaveData");
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
        string data = DataLoad(path);
        PlayerData playerData = JsonUtility.FromJson<PlayerData>(data);

        CurrentStateData = playerData.StateData;
        CurrentSkillData = playerData.SkillData;

        Managers.Game.delay = playerData.delay;
    }

    private void LoadQuestData()
    {
        if (!LoadFileCheck(questDataFileName)) return;

        string questData = DataLoad(path);
        Managers.Game.questDatas = JsonConvert.DeserializeObject<Dictionary<QuestName, QuestData>>(questData);
    }

    public void LoadSoundData()
    {
        if (!LoadFileCheck(soundDataFileName))
        {
            return;
        }
        else
        {
            string soundData = DataLoad(path);
            this.soundData = JsonUtility.FromJson<SoundData>(soundData);
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
            string stageData = DataLoad(path);
            this.stageData = JsonConvert.DeserializeObject<StageData[]>(stageData);

            Managers.Game.MaxScoreArray = this.stageData;
        }
    }

    private string DataLoad(string filePath)
    {
        string data = File.ReadAllText(filePath);
        byte[] bytes = Convert.FromBase64String(data);
        string decoded = System.Text.Encoding.UTF8.GetString(bytes);
        return decoded;
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

        byte[] bytes = System.Text.Encoding.UTF8.GetBytes(dataStr);
        dataStr = Convert.ToBase64String(bytes);
        File.WriteAllText(path + fileName, dataStr);
        Debug.Log(path + fileName);
    }

    #endregion
}
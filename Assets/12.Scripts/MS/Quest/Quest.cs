using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Quest : MonoBehaviour
{
    [HideInInspector]
    public QuestName key;

    public void Reward()
    {
        DataManager dataManager = Managers.Data;
        QuestData questData = Managers.Game.questDatas[key];
        float rewardValue = questData.Rewards.value;
        switch (questData.Rewards.questReward)
        {
            case QuestReward.HealthUp:
                dataManager.CurrentStateData.AdditionalHealth += (int)rewardValue;
                break;

            case QuestReward.SkillExtendedDistance:
                dataManager.CurrentSkillData.DistanceModifier += rewardValue;
                break;

            case QuestReward.SkillGaugIncrementUp:
                dataManager.CurrentStateData.SkillGaugeModifier += rewardValue;
                break;

            case QuestReward.SkillSpeedDown:
                dataManager.CurrentSkillData.SpeedModifier += rewardValue;
                break;
            case QuestReward.SpeedUp:
                dataManager.CurrentStateData.SpeedModifier += rewardValue;
                break;
        }
        questData.IsReceive = true;
        Managers.Data.CurrentStateData.Level += 1; // 레벨 1 증가
        Managers.Game.CallLevel();
        Managers.Data.SaveQuestData();
        Managers.Data.SavePlayerData();
        transform.GetComponentInChildren<Image>().color = new Color(255, 255, 255, 0.7f);
    }
}


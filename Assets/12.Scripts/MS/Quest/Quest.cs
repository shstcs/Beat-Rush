using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
                Debug.Log($"체력 증가! 현재 체력 : {dataManager.CurrentStateData.GetHealth()}");
                break;

            case QuestReward.SkillExtendedDistance:
                dataManager.CurrentSkillData.DistanceModifier += rewardValue;
                Debug.Log($"스킬 거리 증가! 현재 거리 : {dataManager.CurrentSkillData.GetDistance()}");
                break;

            case QuestReward.SkillGaugIncrementUp:
                dataManager.CurrentStateData.SkillGaugeModifier += rewardValue;
                Debug.Log($"게이지 증가량 증가! 현재 게이지 증가량 : {dataManager.CurrentStateData.GetSkillGaugeIncrement()}");
                break;

            case QuestReward.SkillSpeedDown:
                dataManager.CurrentSkillData.SpeedModifier += rewardValue;
                Debug.Log($"스킬 속도 감소! 현재 스킬 속도 : {dataManager.CurrentSkillData.GetSpeed()}");
                break;
        }
        questData.IsReceive = true;
        Managers.Data.SaveQuestData();
        Managers.Data.SavePlayerData();
    }
}


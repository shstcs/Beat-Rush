using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quest : MonoBehaviour
{
    [HideInInspector]
    public QuestData Data;

    public void Reward()
    {
        DataManager data = Managers.Data;
        float rewardValue = this.Data.Rewards.value;
        switch (this.Data.Rewards.questReward)
        {
            case QuestReward.HealthUp:
                data.CurrentStateData.AdditionalHealth += (int)rewardValue;
                Debug.Log($"체력 증가! 현재 체력 : {data.CurrentStateData.GetHealth()}");
                break;

            case QuestReward.SkillExtendedDistance:
                data.CurrentSkillData.DistanceModifier += rewardValue;
                Debug.Log($"스킬 거리 증가! 현재 거리 : {data.CurrentSkillData.GetDistance()}");
                break;

            case QuestReward.SkillGaugIncrementUp:
                data.CurrentStateData.SkillGaugeModifier += rewardValue;
                Debug.Log($"게이지 증가량 증가! 현재 게이지 증가량 : {data.CurrentStateData.GetSkillGaugeIncrement()}");
                break;

            case QuestReward.SkillSpeedDown:
                data.CurrentSkillData.SpeedModifier += rewardValue;
                Debug.Log($"스킬 속도 감소! 현재 스킬 속도 : {data.CurrentSkillData.GetSpeed()}");
                break;
        }
        this.Data.IsReceive = true;
    }
}


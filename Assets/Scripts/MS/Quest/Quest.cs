using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quest : MonoBehaviour
{
    [HideInInspector]
    public QuestData Data;

    public void Reward()
    {
        Player player = Managers.Player;
        float rewardValue = Data.Rewards.value;
        switch (Data.Rewards.questReward)
        {
            case QuestReward.HealthUp:
                player.CurrentStateData.AdditionalHealth += (int)rewardValue;
                Debug.Log($"체력 증가! 현재 체력 : {player.CurrentStateData.GetHealth()}");
                break;

            case QuestReward.SkillExtendedDistance:
                player.CurrentSkillData.DistanceModifier += rewardValue;
                Debug.Log($"스킬 거리 증가! 현재 거리 : {player.CurrentSkillData.GetDistance()}");
                break;

            case QuestReward.SkillGaugIncrementUp:
                player.CurrentStateData.SkillGaugeModifier += rewardValue;
                Debug.Log($"게이지 증가량 증가! 현재 게이지 증가량 : {player.CurrentStateData.GetSkillGaugeIncrement()}");
                break;

            case QuestReward.SkillSpeedDown:
                player.CurrentSkillData.SpeedModifier += rewardValue;
                Debug.Log($"스킬 속도 증가! 현재 스킬 속도 : {player.CurrentSkillData.GetSpeed()}");
                break;
        }
        Data.IsReceive = true;
    }
}


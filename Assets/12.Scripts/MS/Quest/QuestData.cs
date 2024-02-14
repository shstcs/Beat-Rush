using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct Reward
{
    public QuestReward questReward;
    public float value;

    public Reward(QuestReward questReward, float value)
    {
        this.questReward = questReward;
        this.value = value;
    }
}

public class QuestData
{
    public string QuestDesc;
    public string RewardDesc;

    public Reward Rewards;

    public bool IsClear;
    public bool IsReceive;


    public QuestData(string questDesc, string rewardDesc, Reward rewards, bool isClear = false)
    {
        QuestDesc = questDesc;
        RewardDesc = rewardDesc;
        Rewards = rewards;

        IsClear = isClear;
        IsReceive = false;
    }
}

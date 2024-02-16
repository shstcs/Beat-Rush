using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PlayerStateData
{
    [field: Header("StateData")]
    [field: SerializeField] public int Level = 1;
    [field: SerializeField] public int Exp = 0;
    [field: SerializeField][field: Range(0, 10)] public int DeafaultHealth = 10;
    [field: SerializeField] public int AdditionalHealth = 0;
    [field: SerializeField][field: Range(0, 10)] public int CurrentHealth = 10;
    [field: SerializeField][field: Range(0, 100)] public int SkillGauge = 0;
    [field: SerializeField][field: Range(0, 100)] public int SkillGaugeIncrement = 5;
    [field: SerializeField][field: Range(1f, 2f)] public float SkillGaugeModifier = 1f;
    public int CurrentClearStage = 0;

    public int GetSkillGaugeIncrement()
    {
        return (int)(SkillGaugeIncrement * SkillGaugeModifier);
    }

    public int GetHealth()
    {
        return DeafaultHealth + AdditionalHealth;
    }

    public void DeepCopy(PlayerStateData data)
    {
        Level = data.Level;
        Exp = data.Exp;
        DeafaultHealth = data.DeafaultHealth;
        CurrentHealth = data.CurrentHealth;
        AdditionalHealth = data.AdditionalHealth;
        SkillGauge = data.SkillGauge;
        SkillGaugeIncrement = data.SkillGaugeIncrement;
        SkillGaugeModifier = data.SkillGaugeModifier;
        CurrentClearStage = data.CurrentClearStage;
    }
}

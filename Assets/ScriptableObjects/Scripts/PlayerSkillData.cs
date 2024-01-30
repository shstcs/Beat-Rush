using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[Serializable]
public class PlayerSkillData
{
    [field: SerializeField][field: Range(1f, 25f)] public float BaseSpeed = 20f;
    [field: SerializeField][field: Range(1f, 25f)] public float BaseDistance = 20f;
    [field: SerializeField][field: Range(0.5f, 1f)] public float SpeedModifier = 1f;
    [field: SerializeField][field: Range(1f, 2f)] public float DistanceModifier = 1f;

    public float GetSpeed()
    {
        return BaseSpeed * SpeedModifier;
    }

    public float GetDistance()
    {
        return BaseDistance * DistanceModifier;
    }
}

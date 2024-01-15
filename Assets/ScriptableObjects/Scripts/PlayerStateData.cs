using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PlayerStateData
{
    [field: Header("StateData")]
    [field: SerializeField] public float Level { get; private set; } = 1f;
    [field: SerializeField][field: Range(0f, 10f)] public float MaxHealth { get; private set; } = 10f;
    [field: SerializeField][field: Range(0f, 100f)] public float SkillGauge { get; private set; } = 0f;
}

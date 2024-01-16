using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PlayerStateData
{
    [field: Header("StateData")]
    [field: SerializeField] public int Level = 1;
    [field: SerializeField][field: Range(0f, 10f)] public int Health = 10;
    [field: SerializeField][field: Range(0f, 100f)] public int SkillGauge = 0;
}

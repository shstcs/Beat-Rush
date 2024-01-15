using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="Player", menuName = "Characters/Player")]
public class PlayerSO : ScriptableObject
{
    [field: SerializeField] public PlayerBaseData BaseData { get; private set; }   
    [field: SerializeField] public PlayerAttackData AttackData { get; private set; }   
    [field: SerializeField] public PlayerStateData StateData { get; private set; }   
}

using System;
using UnityEngine;

[Serializable]
public class PlayerAnimationData
{
    [SerializeField] private string idleParameterName = "Idle";

    [SerializeField] private string walkParameterName = "Walk";
    [SerializeField] private string runParameterName = "Run";

    [SerializeField] private string attackParameterName = "@Attack";
    [SerializeField] private string attackTriggerParameterName = "Attack";
    [SerializeField] private string ComboParameterName = "Combo";
    [SerializeField] private string ComboAttackParameterName = "ComboAttack";

    [SerializeField] private string deathParameterName = "@Death";
    [SerializeField] private string skillParameterName = "Skill";

    public int IdleParameterHash { get; private set; }
    public int WalkParameterHash { get; private set; }
    public int RunParameterHash { get; private set; }
    public int AttackParameterHash { get; private set; }
    public int AttackTriggerParameterHash { get; private set; }
    public int ComboParameterHash { get; private set; }
    public int ComboAttackParameterHash { get; private set; }
    public int DeathParameterHash { get; private set; }
    public int SkillParameterHash { get; private set; }

    public void Initialize()
    {
        IdleParameterHash = Animator.StringToHash(idleParameterName);

        WalkParameterHash = Animator.StringToHash(walkParameterName);
        RunParameterHash = Animator.StringToHash(runParameterName);

        AttackParameterHash = Animator.StringToHash(attackParameterName);
        AttackTriggerParameterHash = Animator.StringToHash(attackTriggerParameterName);
        ComboParameterHash = Animator.StringToHash(ComboParameterName);
        ComboAttackParameterHash = Animator.StringToHash(ComboAttackParameterName);

        DeathParameterHash = Animator.StringToHash(deathParameterName);
        SkillParameterHash = Animator.StringToHash(skillParameterName);
    }
}

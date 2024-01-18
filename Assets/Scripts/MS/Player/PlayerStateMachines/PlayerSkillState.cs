using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerSkillState : PlayerBaseState
{
    public PlayerSkillState(PlayerStateMachine playerStateMachine) : base(playerStateMachine)
    {
    }

    public override void Enter()
    {
        stateMachine.lockType = InputLockType.Lock;
        base.Enter();
        stateMachine.Player.Animator.SetTrigger(stateMachine.Player.AnimationData.SkillParameterHash);
        stateMachine.Player.CurrentStateData.SkillGauge = 0;
        stateMachine.Player.Skill();
    }

    public override void Update()
    {
        base.Update();

        float normalizedTime = GetNormalizeTime(stateMachine.Player.Animator, "Skill");
        if (normalizedTime >= 1f)
        {
            stateMachine.lockType = InputLockType.UnLock;
            stateMachine.ChangeState(stateMachine.IdleState);
        }
    }
}

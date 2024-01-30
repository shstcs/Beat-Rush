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
        Managers.Game.lockType = InputLockType.Lock;
        base.Enter();
        stateMachine.Player.Animator.SetTrigger(stateMachine.Player.AnimationData.SkillParameterHash);
        stateMachine.Player.CurrentStateData.SkillGauge = 0;
        stateMachine.Player.Skill();
    }

    public override void Update()
    {
        base.Update();

        AnimatorStateInfo currentInfo = stateMachine.Player.Animator.GetCurrentAnimatorStateInfo(0);
        if (!currentInfo.IsTag("Skill") || 0.9 <= currentInfo.normalizedTime)
        {
            stateMachine.ChangeState(stateMachine.IdleState, true);
            Managers.Game.lockType = InputLockType.UnLock;
        }  
    }
}

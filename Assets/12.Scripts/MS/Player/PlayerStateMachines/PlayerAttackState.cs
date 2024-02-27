using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackState : PlayerBaseState
{
    public PlayerAttackState(PlayerStateMachine playerStateMachine) : base(playerStateMachine)
    {
    }

    public override void Enter()
    {
        
        stateMachine.MoveSpeedModifier = 0f;
        base.Enter();
        Managers.Game.lockType = InputLockType.Lock;
        stateMachine.Player.Animator.SetTrigger(stateMachine.Player.AnimationData.AttackTriggerParameterHash);
        stateMachine.Player.SwordEffect.Play();
        Managers.Sound.PlaySFX(SFX.Attack );

        StartAnimation(stateMachine.Player.AnimationData.AttackParameterHash);

    }

    public override void Exit()
    {
        base.Exit();

        StopAnimation(stateMachine.Player.AnimationData.AttackParameterHash);


    }

    public override void Update()
    {
        base.Update();

        CheckDie();
        AnimatorStateInfo currentInfo = stateMachine.Player.Animator.GetCurrentAnimatorStateInfo(0);
        if (!currentInfo.IsTag("Attack") || 0.9 <= currentInfo.normalizedTime)
        {
            stateMachine.ChangeState(stateMachine.IdleState, true);
            Managers.Game.lockType = InputLockType.UnLock;
        }
    }

}


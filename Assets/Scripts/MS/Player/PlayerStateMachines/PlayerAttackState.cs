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

        stateMachine.Player.Animator.SetTrigger(stateMachine.Player.AnimationData.AttackTriggerParameterHash);
        stateMachine.Player.SwordEffect.Play();

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

        float normalizedTime = GetNormalizeTime(stateMachine.Player.Animator, "Attack");

        if (normalizedTime >= 1f)
        {
                stateMachine.ChangeState(stateMachine.IdleState);
            
        }
    }

}


using UnityEngine;

public class PlayerIdleState : PlayerBaseState
{
    public PlayerIdleState(PlayerStateMachine playerStateMachine) : base(playerStateMachine)
    {

    }

    public override void Enter()
    {
        stateMachine.MoveSpeedModifier = 0f;
        base.Enter();
        StartAnimation(stateMachine.Player.AnimationData.IdleParameterHash);
    }

    public override void Exit()
    {
        base.Enter();
        StopAnimation(stateMachine.Player.AnimationData.IdleParameterHash);
    }

    public override void Update()
    {
        base.Update();

        CheckDie();

        //CheckAttacking();

        if (stateMachine.MoveInput != Vector2.zero)
        {

            if (stateMachine.IsRun)
            {
                stateMachine.ChangeState(stateMachine.RunState);
                return;
            }

            OnMove();
        }
    }
}

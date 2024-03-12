using UnityEngine;

public class PlayerStateMachine : StateMachine
{
    public Player Player { get; }

    public PlayerIdleState IdleState { get; }
    public PlayerWalkState WalkState { get; }
    public PlayerRunState RunState { get; }

    public PlayerAttackState attackState { get; }
    public PlayerDeathState deathState { get; }
    public PlayerSkillState skillState { get; }
    public Vector2 MoveInput { get; set; }
    public float MoveSpeed { get; private set; }
    public float RotationDamping { get; private set; }
    public float MoveSpeedModifier { get; set; } = 1f;

    public bool IsRun { get; set; }
    public bool IsDie { get; set; }
    public int ComboIndex { get; set; }
    public Transform MainCameraTransform { get; set; }

    public PlayerStateMachine(Player player)
    {
        this.Player = player;
        IdleState = new PlayerIdleState(this);
        WalkState = new PlayerWalkState(this);
        RunState = new PlayerRunState(this);

        attackState = new PlayerAttackState(this);

        deathState = new PlayerDeathState(this);

        skillState = new PlayerSkillState(this);

        MainCameraTransform = Camera.main.transform;

        MoveSpeed = player.Data.BaseData.BaseSpeed * Managers.Data.CurrentStateData.SpeedModifier;
        RotationDamping = player.Data.BaseData.BaseRotationDamping;
    }
}

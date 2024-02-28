using UnityEngine;

public class Golem : Monster
{
    private Animator _animator;
    private MonsterAnimationData _golemAnimation = new();

    protected override void Awake()
    {
        _animator = GetComponent<Animator>();
        _golemAnimation.Init();

        base.Awake();
    }

    public override void RandomAttack()
    {
        _animator.SetTrigger(_golemAnimation.GetRandomAttackHash());
        base.RandomAttack();
    }

    public override void EndStage()
    {
        _animator.SetBool(_golemAnimation.DieParameterHash, true);

        base.EndStage();
    }
}

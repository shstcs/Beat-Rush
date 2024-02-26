using UnityEngine;

public class FrostDragon : Monster
{
    private Animator _animator;
    private MonsterAnimationData _frostDragonAnimation = new();

    protected override void Awake()
    {
        _animator = GetComponent<Animator>();
        _frostDragonAnimation.Init();

        base.Awake();
    }

    public override void RandomAttack()
    {
        _animator.SetTrigger(_frostDragonAnimation.GetRandomAttackHash());

        base.RandomAttack();
    }

    public override void EndStage()
    {
        _animator.SetBool(_frostDragonAnimation.DieParameterHash, true);

        base.EndStage();
    }
}

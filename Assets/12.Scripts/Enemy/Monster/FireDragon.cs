using UnityEngine;

public class FireDragon : Monster
{
    private Animator _animator;
    private MonsterAnimationData _fireDragonAnimation = new();

    protected override void Awake()
    {
        _animator = GetComponent<Animator>();
        _fireDragonAnimation.Init();

        base.Awake();
    }

    public override void RandomAttack()
    {
        _animator.SetTrigger(_fireDragonAnimation.GetRandomAttackHash());

        base.RandomAttack();
    }

    public override void EndStage()
    {
        _animator.SetBool(_fireDragonAnimation.DieParameterHash, true);

        base.EndStage();
    }
}

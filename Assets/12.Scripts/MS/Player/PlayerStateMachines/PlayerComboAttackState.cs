using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerComboAttackState : PlayerAttackState
{
    private bool alreadyAppliedForce;
    private bool alreadyApplyCombo;

    AttackInfoData attackInfoData;

    public PlayerComboAttackState(PlayerStateMachine playerStateMachine) : base(playerStateMachine)
    {
    }

    //public override void Enter()
    //{
    //    base.Enter();
    //    //Debug.Log("Init");
    //    StartAnimation(stateMachine.Player.AnimationData.ComboAttackParameterHash);
    //    alreadyApplyCombo = false;
    //    alreadyAppliedForce = false;

    //    int comboIndex = stateMachine.ComboIndex;
    //    attackInfoData = stateMachine.Player.Data.AttackData.GetAttackInfo(comboIndex);
    //    stateMachine.Player.Animator.SetInteger("Combo", comboIndex);
    //}

    //public override void Exit()
    //{
    //    base.Exit();
    //    StopAnimation(stateMachine.Player.AnimationData.ComboAttackParameterHash);

    //    if (!alreadyApplyCombo)
    //        stateMachine.ComboIndex = 0;
    //}

    //private void ComboAttack()
    //{
    //    if (alreadyApplyCombo) return;
    //    if (attackInfoData.ComboStateIndex == -1) return;
    //    if (!stateMachine.IsAttacking) return;
    //    alreadyApplyCombo = true;
    //}

    //private void ApplyForce()
    //{
    //    if (alreadyAppliedForce) return;
    //    alreadyAppliedForce = true;

    //    var forceDiection = stateMachine.Player.transform.forward;
    //    stateMachine.Player.Controller.Move(forceDiection * attackInfoData.Force * Time.deltaTime);
    //}

    //public override void Update()
    //{
    //    base.Update();

    //    float normalizedTime = GetNormalizeTime(stateMachine.Player.Animator, "Attack");

    //    if (normalizedTime < 1f)  // 이미 애니메이션이 처리 중
    //    {
    //        if (normalizedTime >= attackInfoData.ForceTransitionTime)
    //            ApplyForce();
    //        if (normalizedTime >= attackInfoData.ComboTransitionTime)
    //            ComboAttack();
    //    }
    //    else // 애니메이션 처리 완료
    //    {
    //        Debug.Log(alreadyApplyCombo);
    //        if (alreadyApplyCombo)
    //        {
    //            stateMachine.ComboIndex = attackInfoData.ComboStateIndex;
    //            stateMachine.ChangeState(stateMachine.ComboAttackState);
    //        }
    //        else
    //        {
    //            stateMachine.ChangeState(stateMachine.IdleState);
    //        }
    //    }
    //}
}

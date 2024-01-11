using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class PlayerBaseState : IState
{
    protected PlayerStateMachine stateMachine;
    protected readonly PlayerData baseData;

    public PlayerBaseState(PlayerStateMachine playerStateMachine)
    {
        stateMachine = playerStateMachine;
        baseData = stateMachine.Player.Data.BaseData;
    }

    public virtual void Enter()
    {
        AddInputActionsCallbacks();
    }

    public virtual void Exit()
    {
        RemoveInputActionsCallbacks();
    }

    public virtual void HandleInput()
    {
        ReadMoveInput();
    }


    public virtual void PhysicsUpdate() { }

    public virtual void Update()
    {
        Move();
    }


    protected virtual void AddInputActionsCallbacks()
    {
        PlayerInput input = stateMachine.Player.Input;
        input.PlayerActions.Move.canceled += OnMoveCanceled;
        input.PlayerActions.Run.performed += OnRunStarted;
        input.PlayerActions.Run.canceled += OnRunCaneled;
    }


    public virtual void RemoveInputActionsCallbacks()
    {
        PlayerInput input = stateMachine.Player.Input;
        //input.PlayerActions.Move.canceled -= OnMoveCanceled;
        //input.PlayerActions.Run.performed -= OnRunStarted;
        //input.PlayerActions.Run.canceled -= OnRunCaneled;
    }
    private void OnRunCaneled(InputAction.CallbackContext context)
    {
        if (stateMachine.MoveInput != Vector2.zero)
            stateMachine.ChangeState(stateMachine.WalkState);
    }

    protected virtual void OnMoveCanceled(InputAction.CallbackContext context)
    {
        if (stateMachine.MoveInput == Vector2.zero) return;

        stateMachine.ChangeState(stateMachine.IdleState);
    }


    protected virtual void OnRunStarted(InputAction.CallbackContext context)
    {
        
    }

    protected void OnMove()
    {
        stateMachine.ChangeState(stateMachine.WalkState);
    }

    private void ReadMoveInput()
    {
        stateMachine.MoveInput = stateMachine.Player.Input.PlayerActions.Move.ReadValue<Vector2>();
    }

    private void Move()
    {
        Vector3 moveDir = GetMoveDirection();

        Rotate(moveDir);
        Move(moveDir);
    }

    private void Move(Vector3 moveDir)
    {
        float moveSpeed = GetMoveSpeed();
        stateMachine.Player.Controller.Move(moveDir * moveSpeed * Time.deltaTime);

    }

    private float GetMoveSpeed()
    {
        return stateMachine.MoveSpeed * stateMachine.MoveSpeedModifier;
    }

    private Vector3 GetMoveDirection()
    {
        Vector3 forward = stateMachine.MainCameraTransform.forward;
        Vector3 right = stateMachine.MainCameraTransform.right;

        forward.y = 0;
        right.y = 0;

        forward.Normalize();
        right.Normalize();

        return forward * stateMachine.MoveInput.y + right * stateMachine.MoveInput.x;
    }

    private void Rotate(Vector3 moveDir)
    {
        if (moveDir != Vector3.zero)
        {
            Transform playerTransform = stateMachine.Player.transform;
            Quaternion rotation = Quaternion.LookRotation(moveDir);
            playerTransform.rotation = Quaternion.Slerp(playerTransform.rotation, rotation, stateMachine.RotationDamping * Time.deltaTime);
            Camera.main.transform.rotation = Quaternion.Slerp(playerTransform.rotation, rotation, stateMachine.RotationDamping * Time.deltaTime);
        }
    }

    protected void StartAnimation(int animationHash)
    {
        stateMachine.Player.Animator.SetBool(animationHash, true);
    }

    protected void StopAnimation(int animationHash)
    {
        stateMachine.Player.Animator.SetBool(animationHash, false);
    }
}

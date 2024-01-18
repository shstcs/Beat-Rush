using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.XR;

public class Player : MonoBehaviour
{
    [field: Header("References")]
    [field: SerializeField] public PlayerSO Data { get; private set; }

    [field: Header("Animations")]
    [field: SerializeField] public PlayerAnimationData AnimationData { get; private set; }

    public Rigidbody Rigidbody { get; private set; }
    public Animator Animator { get; private set; }
    public PlayerInput Input { get; private set; }

    public CharacterController Controller { get; private set; }

    private PlayerStateMachine _stateMachine;

    [HideInInspector]
    public PlayerStateData CurrentStateData;

    public GameObject SkillPrefab;
    
    private void Awake()
    {
        Managers.Player = this;
        AnimationData.Initialize();

        Rigidbody = GetComponent<Rigidbody>();
        Animator = GetComponentInChildren<Animator>();
        Input = GetComponent<PlayerInput>();
        Controller = GetComponent<CharacterController>();

        _stateMachine = new PlayerStateMachine(this);
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        _stateMachine.ChangeState(_stateMachine.IdleState);
        InitStat();
    }
    
    private void Update()
    {
        _stateMachine.HandleInput();
        _stateMachine.Update();
    }

    private void FixedUpdate()
    {
        _stateMachine.PhysicsUpdate();
    }

    public void Rotate(Transform targetTransform)
    {
        transform.LookAt(targetTransform);
    }

    private void InitStat()
    {
        CurrentStateData.Level = Data.StateData.Level;
        CurrentStateData.Health = Data.StateData.Health;
        CurrentStateData.SkillGauge = Data.StateData.SkillGauge;
    }

    public void ChangeHealth(int amount)
    {
        CurrentStateData.Health += amount;
        Debug.Log(CurrentStateData.Health);
        if (CurrentStateData.Health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        _stateMachine.IsDie = true;
        Managers.Game.CallStageEnd();
    }

    public bool IsDie()
    {
        return _stateMachine.IsDie;
    }

    public void Skill()
    {
        var pos = gameObject.transform.position + new Vector3(0f, 0f, 3f);
        Instantiate(SkillPrefab, pos, Quaternion.identity);
        Animator.SetTrigger(AnimationData.SkillParameterHash);
    }
}

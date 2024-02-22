using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.XR;
using UnityEngine.VFX;

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
    public VisualEffect SwordEffect;

    public bool IsUseSkill = false;

    public GameObject SkillPrefab;

    // public SoundManager SoundManager;

    private void Awake()
    {
        Managers.Player = this;

        AnimationData.Initialize();

        Rigidbody = GetComponent<Rigidbody>();
        Animator = GetComponentInChildren<Animator>();
        Input = GetComponent<PlayerInput>();
        Controller = GetComponent<CharacterController>();

        _stateMachine = new PlayerStateMachine(this);
        SwordEffect = GetComponentInChildren<VisualEffect>();
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        _stateMachine.ChangeState(_stateMachine.IdleState);

        Managers.Player = this;

        Input.PlayerActions.Quest.started += OpenQuestWindow;
        Input.PlayerActions.Popup.started += Managers.Popup.OffPopupWindow;

        if (Managers.Game.GameType == GameType.Lobby)
        {
            Controller.enabled = false;
            transform.position = Managers.Game.PlayerSpwanPosition;
            Controller.enabled = true;
            transform.rotation = Managers.Game.PlayerSpwanRotation;
        }
    }


    private void Update()
    {
        _stateMachine.HandleInput();
        _stateMachine.Update();

        Managers.Data.CurrentStateData.PlayTime += Time.unscaledDeltaTime;

        if(Managers.Data.CurrentStateData.PlayTime >= 900f)
        {
            QuestManager.instance.SetQuestClear(QuestName.PlayTime);
        }
    }

    private void FixedUpdate()
    {
        _stateMachine.PhysicsUpdate();
    }

    private void OnDestroy()
    {
        Input.PlayerActions.Quest.started -= OpenQuestWindow;
        Input.PlayerActions.Popup.started -= Managers.Popup.OffPopupWindow;
    }

    private void OpenQuestWindow(InputAction.CallbackContext context)
    {
        if (Managers.Game.GameType == GameType.Play) return;

        QuestManager.instance.OpenQuest();
    }

    public void Rotate(Transform targetTransform)
    {
        var targetPos = new Vector3(targetTransform.position.x, transform.position.y, targetTransform.position.z);
        transform.LookAt(targetPos);
    }

    public void ChangeHealth(int amount)
    {
        if (_stateMachine.IsDie) return;
        Managers.Data.CurrentStateData.CurrentHealth += amount;
        if (Managers.Data.CurrentStateData.CurrentHealth <= 0)
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
        var pos = gameObject.transform.position + new Vector3(0f, 0.5f, 0.01f);
        var obj = Instantiate(SkillPrefab, pos, Quaternion.identity);
        Rotate(obj.transform);
        Managers.Sound.PlaySFX(SFX.Skill);
        IsUseSkill = true;
    }

    public void ChangeIdleState()
    {
        _stateMachine.ChangeState(_stateMachine.IdleState);
    }
}

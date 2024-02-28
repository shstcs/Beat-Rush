using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.VFX;

public class Note : MonoBehaviour
{
    private ParticleSystem _particle;
    [HideInInspector]
    public int noteNumber = 0;
    public int stage = 0;
    public bool isTrap = false;

    [HideInInspector]
    public bool IsJudgeNoteCreated;
    [HideInInspector]
    public GameObject LeftJudgeNote;
    [HideInInspector]
    public GameObject RightJudgeNote;

    private VisualEffectAsset _effect;
    private VisualEffectAsset _trapEffect;
    protected void Awake()
    {
        GameObject particlePrefab;

        particlePrefab = Managers.Resource.Load<GameObject>($"PAT{Managers.Game.currentStage}");
        _particle = particlePrefab.GetComponent<ParticleSystem>();

        if (Managers.Game.currentStage == 3)
        {
            _effect = Resources.Load<VisualEffectAsset>("Fireball");
            _trapEffect = Resources.Load<VisualEffectAsset>("Fireball_Trap");
        }
    }

    private void Start()
    {
        _particle.Stop();
    }

    protected void Update()
    {
        if (Time.timeScale > 0)
        {
            if (gameObject.transform.position.z < 8)
            {
                if (isTrap)
                {
                    Managers.Game.Combo++;
                    Managers.Game.AddScore(50 + Managers.Game.Combo);
                    BreakNote();
                }
                else
                {
                    Managers.Game.Combo = 0;
                    BreakNote();
                    Managers.Game.curJudge = "Miss";
                    Managers.Game.judgeNotes[(int)Score.Miss]++;
                    if (Managers.Game.currentStage != 0)
                    {
                        Managers.Player.ChangeHealth(-1);
                        Managers.Game.CallDamaged();
                    }
                }
            }
            else if (gameObject.transform.position.z < 9.5 && isTrap)
            {
                gameObject.GetComponent<Collider>().enabled = false;
            }
        }
    }

    private void OnEnable()
    {
        if (Managers.Game.mode == GameMode.normal)
        {
            ChangeColor();
        }

        gameObject.GetComponent<Collider>().enabled = true;
        if (Managers.Game.mode == GameMode.Sudden)
        {
            foreach (Transform child in transform)
            {
                // 자식 오브젝트 비활성화
                child.gameObject.SetActive(false);
            }
        }
    }

    public void ShowNote()
    {
        foreach (Transform child in transform)
        {
            // 자식 오브젝트 활성화
            child.gameObject.SetActive(true);
        }
        if (Managers.Game.currentStage == 3)
        {
            ChangeColor();
        }
    }

    public void BreakNote()
    {
        ParticleSystem _destroyParticle = Instantiate(_particle);
        _destroyParticle.transform.position = transform.position;
        _particle.Play();
        transform.position = Vector3.zero;
        Managers.Game.stageInfos[stage].curNoteInStage[noteNumber]++;

        IsJudgeNoteCreated = false;
        if(LeftJudgeNote != null)
            LeftJudgeNote.SetActive(false);
        if(LeftJudgeNote != null)
            RightJudgeNote.SetActive(false);

        gameObject.SetActive(false);
    }

    private void ChangeColor()
    {
        if (Managers.Game.currentStage == 3)
        {
            if (isTrap)
            {
                gameObject.GetComponentInChildren<VisualEffect>().visualEffectAsset = _trapEffect;
            }
            else
            {
                gameObject.GetComponentInChildren<VisualEffect>().visualEffectAsset = _effect;
            }
        }
    }
}

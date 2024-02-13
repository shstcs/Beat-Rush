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

    private VisualEffectAsset _effect;
    private VisualEffectAsset _trapEffect;
    protected void Awake()
    {
        GameObject particlePrefab;
        if(Managers.Game.currentStage == 0)
        {
            particlePrefab = Managers.Resource.Load<GameObject>("Spheres Explode");
        }
        else if(Managers.Game.currentStage == 1)
        {
            particlePrefab = Managers.Resource.Load<GameObject>("Blood Splash");
        }
        else if(Managers.Game.currentStage==2)
        {
            particlePrefab = Managers.Resource.Load<GameObject>("Water Splash");
        }
        else
        {
            particlePrefab = Managers.Resource.Load<GameObject>("Fire Splash");
        }
        _particle = particlePrefab.GetComponent<ParticleSystem>();

        if (Managers.Game.currentStage == 3)
        {
            _effect = Managers.Resource.Load<VisualEffectAsset>("Fireball");
            _trapEffect = Managers.Resource.Load<VisualEffectAsset>("Fireball_Trap");
        }
    }

    private void Start()
    {
        _particle.Stop();
    }

    protected void Update()
    {
        if(Time.timeScale > 0)
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
                    }
                }
            }
            else if(gameObject.transform.position.z < 9.5 && isTrap)
            {
                gameObject.GetComponent<Collider>().enabled = false;
            }
        }
    }

    private void OnEnable()
    {
        StartCoroutine(nameof(ChangeColor));
        if(Managers.Game.mode == GameMode.Sudden)
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
    }

    public void HideNote()
    {
        GetComponentInChildren<Transform>().gameObject.SetActive(false);
    }

    public void BreakNote()
    {
        ParticleSystem _destroyParticle = Instantiate(_particle);
        _destroyParticle.transform.position = transform.position;
        _particle.Play();
        transform.position = Vector3.zero;
        Managers.Game.curNoteInStage[stage,noteNumber]++;
        gameObject.SetActive(false);
    }

    private IEnumerator ChangeColor()
    {
        yield return new WaitForSeconds(.5f);

        if (Managers.Game.currentStage == 3)
        {
            if (isTrap)
            {
                gameObject.GetComponent<VisualEffect>().visualEffectAsset = _trapEffect;
            }
            else
            {
                gameObject.GetComponent<VisualEffect>().visualEffectAsset = _effect;
            }
        }
    }
}

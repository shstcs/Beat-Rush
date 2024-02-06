using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class Note : MonoBehaviour
{
    private ParticleSystem _particle;
    [HideInInspector]
    public int noteNumber = 0;
    public int stage = 0;
    public bool isTrap = false;
    protected void Awake()
    {
        if(Managers.Game.currentStage == 0)
        {
            _particle = Resources.Load<ParticleSystem>("Spheres Explode");
        }
        else
        {
            _particle = Resources.Load<ParticleSystem>("Blood Splash");
        }
        
    }

    private void Start()
    {
        _particle.Stop();
    }

    protected void Update()
    {
        if (gameObject.transform.position.z < 8 && Time.timeScale > 0)
        {
            if(isTrap)
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
}

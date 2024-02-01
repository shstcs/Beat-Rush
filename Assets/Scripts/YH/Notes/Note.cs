using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Note : MonoBehaviour
{
    private ParticleSystem _particle;
    [HideInInspector]
    public int noteNumber = 0;
    public int stage = 0;
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
            Managers.Game.Combo = 0;
            BreakNote();
            Managers.Player.ChangeHealth(-1);
            Managers.Game.judgeNotes[(int)Score.Miss]++;
            Managers.Game.curJudge = "Miss";
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

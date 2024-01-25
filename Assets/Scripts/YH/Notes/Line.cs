using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class Line : MonoBehaviour
{
    public LayerMask noteLayer;
    void Update()
    {
        
        //if (Input.GetKeyDown(KeyCode.Mouse0) || Input.GetKeyDown(KeyCode.Z))
        //{
        //    if (Managers.Player.IsDie()) return;

        //    CheckNotes();
        //}
    }

    private void Start()
    {
        Managers.Player.Input.PlayerActions.Attack.started += OnAttackStarted;
    }

    private void OnAttackStarted(InputAction.CallbackContext context)
    {
        if (Managers.Player.IsDie()) return;
        CheckNotes();
    }

    private void CheckNotes()
    {
        Collider[] colliders = Physics.OverlapBox(transform.position, new Vector3(10, 5, 0.01f) / 2, Quaternion.identity, noteLayer);
        if (colliders.Length > 0)
        {
            colliders = colliders.OrderBy(collider => collider.transform.position.z).ToArray();

            Managers.Player.Rotate(colliders[0].transform);     

            float distance = Mathf.Abs(10 - colliders[0].transform.position.z);

            int score;
            if (distance > 0.7f)        //Bad
            {
                score = 10;
                Managers.Game.judgeNotes[(int)Score.Bad]++;
            }
            else if (distance > 0.5f)    //Good
            {
                score = 30;
                Managers.Game.judgeNotes[(int)Score.Good]++;
            }
            else if (distance > 0.1f)     //Great
            {
                score = 50;
                Managers.Game.judgeNotes[(int)Score.Great]++;
            }
            else                        //Perfect
            {
                score = 100;
                Managers.Game.judgeNotes[(int)Score.Perfect]++;
            } 

            colliders[0].GetComponent<Note>().BreakNote();

            Managers.Game.Combo++;
            Managers.Game.MaxCombo = Managers.Game.Combo > Managers.Game.MaxCombo ? Managers.Game.Combo : Managers.Game.MaxCombo;       //나중에 리팩토링
            Managers.Game.AddScore(score + Managers.Game.Combo);     

            // Skill
            if(Managers.Player.CurrentStateData.SkillGauge < 100)
                Managers.Player.CurrentStateData.SkillGauge += 10;
        }
        else
        {
            Managers.Game.Combo = 0;
        }
    }

}

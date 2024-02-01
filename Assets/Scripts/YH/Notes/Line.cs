using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class Line : MonoBehaviour
{
    public LayerMask noteLayer;

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

            if (Managers.Game.currentStage == 0) // 싱크 조절
            {
                Managers.Game.delay = (colliders[0].transform.position.z - 10) > 0 ?
                    Managers.Game.delay -= 0.05f : Managers.Game.delay += 0.05f;
                Debug.Log(Managers.Game.delay);
            }

            float distance = Mathf.Abs(10 - colliders[0].transform.position.z);

            int score;
            if (distance > 0.7f)        //Bad
            {
                score = 10;
                Managers.Game.judgeNotes[(int)Score.Bad]++;
                Managers.Game.curJudge = "Bad";
                Managers.Game.Combo = 0;
            }
            else if (distance > 0.5f)    //Good
            {
                score = 30;
                Managers.Game.judgeNotes[(int)Score.Good]++;
                Managers.Game.curJudge = "Good";
            }
            else if (distance > 0.1f)     //Great
            {
                score = 50;
                Managers.Game.judgeNotes[(int)Score.Great]++;
                Managers.Game.curJudge = "Great";
            }
            else                        //Perfect
            {
                score = 100;
                Managers.Game.judgeNotes[(int)Score.Perfect]++;
                Managers.Game.curJudge = "Perfect";
            } 

            colliders[0].GetComponent<Note>().BreakNote();

            Managers.Game.Combo++;
            Managers.Game.MaxCombo = Managers.Game.Combo > Managers.Game.MaxCombo ? Managers.Game.Combo : Managers.Game.MaxCombo;       //나중에 리팩토링
            Managers.Game.AddScore(score + Managers.Game.Combo);
            if (Managers.Game.Combo == 100)
            {
                QuestManager.instance.SetQuestClear(QuestName.Stage100Combo);
            }

            // Skill
            if (Managers.Data.CurrentStateData.SkillGauge < 100)
                Managers.Data.CurrentStateData.SkillGauge += Managers.Data.CurrentStateData.GetSkillGaugeIncrement();
        }
    }

}

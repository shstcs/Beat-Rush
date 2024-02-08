using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Line : MonoBehaviour
{
    public LayerMask noteLayer;
    public Image CircleImage;

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

                Managers.Game.delay = (colliders[0].transform.position.z - 10) > 0 ? Managers.Game.delay -= 0.05f : Managers.Game.delay += 0.05f;
                Managers.Game.delay = Managers.Game.delay -= (colliders[0].transform.position.z - 10) / 4;

                Debug.Log(Managers.Game.delay);
            }

            if (colliders[0].GetComponent<Note>().isTrap)   // 함정노트
            {
                Managers.Game.Combo = 0;
                colliders[0].GetComponent<Note>().BreakNote();

                Managers.Game.curJudge = "Miss";
                Managers.Game.judgeNotes[(int)Score.Miss]++;

                if (Managers.Game.currentStage != 0)
                {
                    Managers.Player.ChangeHealth(-1);
                }
            }
            else                                            // 일반노트
            {
                float distance = Mathf.Abs(10 - colliders[0].transform.position.z);
                float colliderSize = colliders[0].bounds.size.z / 2;

                int score;
                if (distance > colliderSize * 0.8f)        //Bad
                {
                    score = 10;
                    Managers.Game.judgeNotes[(int)Score.Bad]++;
                    Managers.Game.curJudge = "Bad";
                    Managers.Game.Combo = 0;
                }
                else if (distance > colliderSize * 0.5f)    //Good
                {
                    score = 30;
                    Managers.Game.judgeNotes[(int)Score.Good]++;
                    Managers.Game.curJudge = "Good";
                    CircleImage.GetComponent<Animator>().SetTrigger("Boom");
                }
                else if (distance > colliderSize * 0.3f)     //Great
                {
                    score = 50;
                    Managers.Game.judgeNotes[(int)Score.Great]++;
                    Managers.Game.curJudge = "Great";
                    CircleImage.GetComponent<Animator>().SetTrigger("Boom");
                }
                else                        //Perfect
                {
                    score = 100;
                    Managers.Game.judgeNotes[(int)Score.Perfect]++;
                    Managers.Game.curJudge = "Perfect";
                    CircleImage.GetComponent<Animator>().SetTrigger("Boom");
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
                if (Managers.Data.CurrentStateData.SkillGauge < 100 && Managers.Game.currentStage != 0)
                    Managers.Data.CurrentStateData.SkillGauge += Managers.Data.CurrentStateData.GetSkillGaugeIncrement();
            }
        }
        Managers.Game.SetRank();
    }

}

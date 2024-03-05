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
        Managers.Player.Input.PlayerActions.Attack1.started += OnAttackStarted;
        Managers.Player.Input.PlayerActions.Attack2.started += OnAttackStarted;
        Managers.Player.Input.PlayerActions.Attack3.started += OnAttackStarted;
        Managers.Player.Input.PlayerActions.Attack4.started += OnAttackStarted;
        Managers.Player.Input.PlayerActions.Attack5.started += OnAttackStarted;
    }

    private void OnAttackStarted(InputAction.CallbackContext context)
    {
        if (Managers.Player.IsDie() || Time.timeScale == 0) return;
        CheckNotes();
    }

    private void MissNote()
    {
        Managers.Game.curJudge = "Miss";
        Managers.Game.judgeNotes[(int)Score.Miss]++;
        Managers.Game.Combo = 0;

        if (Managers.Game.currentStage != 0)
        {
            Managers.Player.ChangeHealth(-1);
            Managers.Game.CallDamaged();
        }
    }

    private void CheckNotes()
    {
        Collider[] colliders = Physics.OverlapBox(transform.position, new Vector3(10, 5, 0.01f) / 2, Quaternion.identity, noteLayer);
        if (colliders.Length > 0)
        {
            colliders = colliders.OrderBy(collider => collider.transform.position.z).ToArray();
            Collider selectCollider = colliders[0].bounds.size.z > 2 ? colliders[0] : colliders[1];

            Managers.Player.Rotate(selectCollider.transform);

            //if (Managers.Game.currentStage == 0) // 싱크 조절
            //{
            //    Managers.Game.delay = Managers.Game.delay -= (selectCollider.transform.position.z - 10) / 4;
            //}

            if (selectCollider.GetComponent<Note>().mode == 1)   // 함정노트
            {
                selectCollider.GetComponent<Note>().BreakNote();

                MissNote();
            }
            else                                            // 일반노트
            {
                float distance = Mathf.Abs(10 - selectCollider.transform.position.z);
                float colliderSize = selectCollider.bounds.size.z / 2;
                int score;
                if (distance > colliderSize * 0.8f)        //Miss
                {
                    score = 0;
                    MissNote();
                }
                else if (distance > colliderSize * 0.7f)        //Bad
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
                else if (distance > colliderSize * 0.2f)     //Great
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

                selectCollider.GetComponent<Note>().BreakNote();

                Managers.Game.Combo++;
                Managers.Game.MaxCombo = Managers.Game.Combo > Managers.Game.MaxCombo ? Managers.Game.Combo : Managers.Game.MaxCombo;       //나중에 리팩토링
                if (Managers.Game.mode == GameMode.Sudden)
                {
                    Managers.Game.AddScore((int)(score * Managers.Game.speedModifier * 1.5f) + Managers.Game.Combo);        //돌발모드
                }
                else
                {
                    Managers.Game.AddScore((int)(score * Managers.Game.speedModifier) + Managers.Game.Combo);               //일반모드
                }

                // Skill
                if (Managers.Data.CurrentStateData.SkillGauge < 100 && Managers.Game.currentStage != 0)
                    Managers.Data.CurrentStateData.SkillGauge += Managers.Data.CurrentStateData.GetSkillGaugeIncrement();
            }
            Managers.Game.CallCheckNote();
        }
        Managers.Game.SetRank();
    }

}

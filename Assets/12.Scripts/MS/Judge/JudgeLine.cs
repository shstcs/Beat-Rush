using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JudgeLine : MonoBehaviour
{
    public LayerMask NoteLayer;
    public RectTransform JudgeNoteSpwanPos;
    public GameObject JudgeNote;

    private const float _judgeNoteSpawnX = 850f;


    private void OnTriggerEnter(Collider other)
    {
        if (NoteLayer.value == (NoteLayer.value |(1 << other.gameObject.layer)))
        {
            var note = other.gameObject.GetComponent<Note>();
            if (note.IsJudgeNoteCreated) return;

            note.IsJudgeNoteCreated = true;

            GameObject leftJudgeNote = Managers.Pool.SpawnFromJudgePool();
            leftJudgeNote.transform.SetParent(JudgeNoteSpwanPos);
            leftJudgeNote.GetComponent<RectTransform>().anchoredPosition = new Vector2(_judgeNoteSpawnX, 0f);
            note.LeftJudgeNote = leftJudgeNote;

            GameObject rightJudgeNote = Managers.Pool.SpawnFromJudgePool();
            rightJudgeNote.transform.SetParent(JudgeNoteSpwanPos);
            rightJudgeNote.GetComponent<RectTransform>().anchoredPosition = new Vector2(-_judgeNoteSpawnX, 0f);
            note.RightJudgeNote = rightJudgeNote;

            if(Managers.Game.mode == GameMode.Sudden)
            {
                other.GetComponent<Note>().ShowNote();
            }
        }
    }
}

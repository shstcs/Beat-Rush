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
            GameObject topJudgeNote = Managers.Pool.SpawnFromJudgePool();
            topJudgeNote.transform.SetParent(JudgeNoteSpwanPos);
            topJudgeNote.GetComponent<RectTransform>().anchoredPosition = new Vector2(_judgeNoteSpawnX, 0f);

            GameObject bottomJudgeNote = Managers.Pool.SpawnFromJudgePool();
            bottomJudgeNote.transform.SetParent(JudgeNoteSpwanPos);
            bottomJudgeNote.GetComponent<RectTransform>().anchoredPosition = new Vector2(-_judgeNoteSpawnX, 0f);

            if(Managers.Game.mode == GameMode.Sudden)
            {
                other.GetComponent<Note>().ShowNote();
            }
        }
    }
}

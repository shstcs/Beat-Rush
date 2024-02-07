using System.Collections;
using System.Collections.Generic;
using System.Net.Mime;
using Unity.VisualScripting;
using UnityEditor.Rendering;
using UnityEngine;

public class JudgeNote : MonoBehaviour
{
    private RectTransform _rect;
    private float time;
    private Vector2 _targetVec = Vector2.zero;

    private const float _judgeNoteSpawnX = 850f;
    private const float _distance = 5.9f;

    private void Awake()
    {
        _rect = GetComponent<RectTransform>();
        time = _distance / (Managers.Game.noteDistance[Managers.Game.currentStage] / (60 / Managers.Game.bpm[Managers.Game.currentStage]));
    }

    private void Update()
    {
        if(_rect.anchoredPosition == _targetVec)
        {
            gameObject.SetActive(false);
        }
    }

    private void FixedUpdate()
    {
        _rect.anchoredPosition = Vector3.MoveTowards(_rect.anchoredPosition, _targetVec, _judgeNoteSpawnX / time * Time.deltaTime);
    }

}

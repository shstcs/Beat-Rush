using System.Collections;
using System.Collections.Generic;
using System.Net.Mime;
using Unity.VisualScripting;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.UI;

public class JudgeNote : MonoBehaviour
{
    private RectTransform _rect;
    private float time;
    private Vector2 _targetVec = Vector2.zero;
    private Image _image;

    private const float _judgeNoteSpawnX = 850f;
    private const float _distance = 10.5f;

    private void Awake()
    {
        _rect = GetComponent<RectTransform>();
        _image = GetComponent<Image>();
        //time = _distance / (Managers.Game.noteDistance[Managers.Game.currentStage] / (60 / Managers.Game.bpm[Managers.Game.currentStage]));
        time = _distance / (Managers.Game.stageInfos[Managers.Game.currentStage].noteSpeed * Managers.Game.speedModifier); //속도 배율에 따라 달라지게 수정
    }

    private void Update()
    {
        if (_rect.anchoredPosition == _targetVec)
        {
            gameObject.SetActive(false);
        }
    }

    private void FixedUpdate()
    {
        _rect.anchoredPosition = Vector3.MoveTowards(_rect.anchoredPosition, _targetVec, _judgeNoteSpawnX / time * Time.deltaTime);
        _image.color = new Color(255, 255, 255, 1f - Mathf.Abs(_rect.anchoredPosition.x) / _judgeNoteSpawnX);
    }

}

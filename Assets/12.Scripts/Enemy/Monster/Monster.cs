using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Monster : MonoBehaviour, IMonster
{
    private List<IPattern> _patterns = new List<IPattern>();
    private int curStage;
    private int _currentPatternIndex = 0;
    private int _currentFeedbackIndex = -1;
    private int _feedbackCount;
    private float _attackDelay;

    protected virtual void Awake()
    {
        curStage = Managers.Game.currentStage;
        _attackDelay = Managers.Game.stageInfos[curStage].PatternLength / (Managers.Game.stageInfos[curStage].noteSpeed);

        for(int i = 1; i <= Managers.Game.stageInfos[curStage].PatternCount; i++)
        {
            _patterns.Add(new IPattern(curStage,i));
        }

        if(curStage != 2) SortPattern();
    }

    private void Update()
    {
        if (Time.timeScale > 0)
        {
            if (_feedbackCount > 12 && _currentFeedbackIndex >= 0)
            {
                if (_currentFeedbackIndex > 0)
                {
                    _patterns[_currentFeedbackIndex - 1].Feedback();
                }

                _patterns[_currentFeedbackIndex].Feedback();
                _feedbackCount = 0;
            }
            _feedbackCount++;
        }
        else
        {
            if (_currentFeedbackIndex >= 0)
            {
                _patterns[_currentFeedbackIndex].Pause();               //다시 시작할 때를 위해
            }
        }
    }

    public void SortPattern()
    {
        for (int i = _patterns.Count - 1; i > 0; i--)
        {
            int randomIndex = Random.Range(0, i + 1);
            (_patterns[randomIndex], _patterns[i]) = (_patterns[i], _patterns[randomIndex]);
        }
    }

    public (int length, float delay, BGM bgm) GetPatternData()
    {
        return (_patterns.Count, _attackDelay, (BGM)curStage);
    }

    public virtual void RandomAttack()
    {
        StartCoroutine(_patterns[_currentPatternIndex++].Attack());
        _currentFeedbackIndex++;
    }

    public virtual void EndStage()
    {
        StartCoroutine(Managers.Sound.VolumeDown());
    }
}

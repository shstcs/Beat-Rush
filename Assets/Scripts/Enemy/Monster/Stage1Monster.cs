using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class Stage1Monster :MonoBehaviour, IMonster
{
    private IPattern[] _patterns;
    private int _currentPatternIndex =0;
    private int _currentFeedbackIndex = -1;
    private int _feedbackCount;

    private void Awake()
    {
        _patterns = new IPattern[]
        {
            new Pattern1(),
            new Pattern2(),
            new Pattern3(),
            new Pattern4()
        };

        foreach (var pattern in _patterns)
        {
            pattern.SetPattern();
        }

        SortPattern();
    }
    
    private void Update()
    {
        if (Time.timeScale > 0)
        {
            if (_feedbackCount > 12 && Managers.Sound.PlayTime() >= 0)
            {
                _patterns[_currentFeedbackIndex].Feedback();
                _feedbackCount = 0;
                Debug.Log("Debug On");
            }
            else if (Managers.Sound.PlayTime() == 0)
            {
                //_patterns[_currentFeedbackIndex].Pause();               //다시 시작할 때를 위해
            }
            _feedbackCount++;
        }
    }

    public void SortPattern()
    {
        Random random = new Random();

        for (int i = _patterns.Length - 1; i > 0; i--)
        {
            int randomIndex = random.Next(0, i + 1);

            IPattern temp = _patterns[i];
            _patterns[i] = _patterns[randomIndex];
            _patterns[randomIndex] = temp;
        }
    }

    public void RandomAttack()           //return값으로 끝나는 신호를 줘볼까?
    {
        if(_currentPatternIndex < _patterns.Length)
        {
            StartCoroutine(_patterns[_currentPatternIndex++].Attack());
            _currentFeedbackIndex++;
        }
        else
        {
            StartCoroutine(Managers.Sound.VolumeDown());
        }
    }
} 

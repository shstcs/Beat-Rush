using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class Stage1Monster :MonoBehaviour
{
    private IPattern[] _patterns;
    private int _currentPatternIndex = 0;

    private void Awake()
    {
        _patterns = new IPattern[]
        {
            new Pattern1(),
            new Pattern2(),
            new Pattern3(),
            new Pattern4()
        };
    }

    private void Start()
    {
        foreach (var pattern in _patterns)
        {
            pattern.SetPattern();
        }
        SortPattern();
    }

    private void SortPattern()
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

    public void RandomAttack(float noteSpeed)           //return값으로 끝나는 신호를 줘볼까?
    {
        if(_currentPatternIndex >= _patterns.Length)
        {
            //StartCoroutine(SoundManager.VolumeDown());
        }
        else
        {
            StartCoroutine(_patterns[_currentPatternIndex].Attack(noteSpeed));
            _currentPatternIndex++;
        }
    }
}

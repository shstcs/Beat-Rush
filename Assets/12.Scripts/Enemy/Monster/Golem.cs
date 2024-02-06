using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Random = System.Random;

public class Golem : MonoBehaviour, IMonster
{
    private Animator _animator;
    [SerializeField] private GameObject _camera;
    private Animator _cameraAnimator;
    private MonsterAnimationData _golemAnimation = new();

    private IPattern[] _patterns;
    private int _currentPatternIndex = 0;
    private int _currentFeedbackIndex = -1;
    private int _feedbackCount;
    private float _attackDelay = 33.3333f;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _cameraAnimator = _camera.GetComponent<Animator>();
        _golemAnimation.Init();

        _patterns = new IPattern[]
        {
            new Pattern1_1(),
            new Pattern1_2(),
            new Pattern1_3(),
            new Pattern1_4()
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
            if (_feedbackCount > 12 && _currentFeedbackIndex >= 0)
            {
                _patterns[_currentFeedbackIndex].Feedback();
                _feedbackCount = 0;
            }
            _feedbackCount++;
        }
        if (Time.timeScale == 0)
        {
            _patterns[_currentFeedbackIndex].Pause();               //다시 시작할 때를 위해
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

    public (int length, float delay, BGM bgm) GetPatternData()
    {
        return (_patterns.Length,_attackDelay, BGM.Stage1);
    }

    public void RandomAttack()           //return값으로 끝나는 신호를 줘볼까?
    {
        if(Time.timeScale != 0)
        {
            if (_currentPatternIndex == 0)
            {
                _cameraAnimator.SetTrigger("ShowBoss");
            }
            StartCoroutine(_patterns[_currentPatternIndex++].Attack());
            _animator.SetTrigger(_golemAnimation.GetRandomAttackHash());
            _currentFeedbackIndex++;
        }
    }

    public void EndStage()
    {
        _animator.SetBool(_golemAnimation.DieParameterHash, true);
        StartCoroutine(Managers.Sound.VolumeDown());
    }
}

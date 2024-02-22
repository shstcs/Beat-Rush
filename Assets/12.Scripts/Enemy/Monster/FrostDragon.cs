using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrostDragon : MonoBehaviour, IMonster
{
    private Animator _animator;
    [SerializeField] private GameObject _camera;
    private Animator _cameraAnimator;
    private MonsterAnimationData _frostDragonAnimation = new();

    private IPattern[] _patterns;
    private int _currentPatternIndex = 0;
    private int _currentFeedbackIndex = -1;
    private int _feedbackCount;
    private float _attackDelay;

    private void Awake()
    {
        _attackDelay = 128f / (Managers.Game.noteSpeed[Managers.Game.currentStage]);
        _animator = GetComponent<Animator>();
        _cameraAnimator = _camera.GetComponent<Animator>();
        _frostDragonAnimation.Init();

        _patterns = new IPattern[]
        {
            new Pattern2_1(),
            new Pattern2_2(),
            new Pattern2_3(),
            new Pattern2_4(),
            new Pattern2_5(),
            new Pattern2_6(),
            new Pattern2_7(),
            new Pattern2_8(),
            new Pattern2_9()
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
        else
        {
            if(_currentFeedbackIndex >= 0)
            {
                _patterns[_currentFeedbackIndex].Pause();               //다시 시작할 때를 위해
            }
        }
    }

    public void SortPattern()
    {

    }

    public (int length, float delay, BGM bgm) GetPatternData()
    {
        return (_patterns.Length, _attackDelay, BGM.Stage2);
    }

    public void RandomAttack()           
    {
        StartCoroutine(_patterns[_currentPatternIndex++].Attack());
        _animator.SetTrigger(_frostDragonAnimation.GetRandomAttackHash());
        _currentFeedbackIndex++;
    }

    public void EndStage()
    {
        _animator.SetBool(_frostDragonAnimation.DieParameterHash, true);
        StartCoroutine(Managers.Sound.VolumeDown());
    }
}

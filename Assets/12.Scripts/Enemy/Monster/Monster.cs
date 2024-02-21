using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour, IMonster
{
    private Animator _animator;
    [SerializeField] private GameObject _camera;
    private Animator _cameraAnimator;
    private MonsterAnimationData _golemAnimation = new();

    protected IPattern[] _patterns;
    private int _currentPatternIndex = 0;
    private int _currentFeedbackIndex = -1;
    private int _feedbackCount;
    private float _attackDelay;

    protected void Awake()
    {
        _animator = GetComponent<Animator>();
        _cameraAnimator = _camera.GetComponent<Animator>();
        _golemAnimation.Init();
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
            if (_currentFeedbackIndex >= 0)
            {
                _patterns[_currentFeedbackIndex].Pause();               //다시 시작할 때를 위해
            }
        }
    }

    public void SortPattern()
    {

        for (int i = _patterns.Length - 1; i > 0; i--)
        {
            int randomIndex = Random.Range(0, i + 1);

            IPattern temp = _patterns[i];
            _patterns[i] = _patterns[randomIndex];
            _patterns[randomIndex] = temp;
        }
    }

    public void EndStage()
    {
        if (Time.timeScale != 0)
        {
            StartCoroutine(_patterns[_currentPatternIndex++].Attack());
            _animator.SetTrigger(_golemAnimation.GetRandomAttackHash());
            _currentFeedbackIndex++;
        }
    }

    public (int length, float delay, BGM bgm) GetPatternData()
    {
        throw new System.NotImplementedException();
    }

    public void RandomAttack()
    {
        throw new System.NotImplementedException();
    }
}

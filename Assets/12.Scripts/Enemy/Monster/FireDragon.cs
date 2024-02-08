using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireDragon : MonoBehaviour, IMonster
{
    private Animator _animator;
    [SerializeField] private GameObject _camera;
    private Animator _cameraAnimator;
    private MonsterAnimationData _fireDragonAnimation = new();

    private IPattern[] _patterns;
    private int _currentPatternIndex = 0;
    private int _currentFeedbackIndex = -1;
    private int _feedbackCount;
    private float _attackDelay;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _cameraAnimator = _camera.GetComponent<Animator>();
        _fireDragonAnimation.Init();
        _attackDelay = 128f / (Managers.Game.noteDistance[Managers.Game.currentStage] / (60 / Managers.Game.bpm[Managers.Game.currentStage]));
        _patterns = new IPattern[]
        {
            new Pattern3_1(),
            new Pattern3_2(),
            new Pattern3_3(),
            new Pattern3_4(),
            new Pattern3_5(),
            new Pattern3_6(),
            new Pattern3_7(),
            new Pattern3_8(),
            new Pattern3_9(),
            new Pattern3_10(),
            new Pattern3_11(),
            new Pattern3_12(),
            new Pattern3_13(),
            new Pattern3_14(),
            new Pattern3_15(),
            new Pattern3_16(),
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
            if (_feedbackCount > 12 && Time.timeScale > 0 && _currentFeedbackIndex >= 0)
            {
                _patterns[_currentFeedbackIndex].Feedback();
                _feedbackCount = 0;
            }
            if (Time.timeScale == 0)
            {
                _patterns[_currentFeedbackIndex].Pause();               //다시 시작할 때를 위해
            }
            _feedbackCount++;
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

    public (int length, float delay, BGM bgm) GetPatternData()
    {
        return (_patterns.Length, _attackDelay, BGM.Stage3);
    }

    public void RandomAttack()           
    {
        StartCoroutine(_patterns[_currentPatternIndex++].Attack());
        _animator.SetTrigger(_fireDragonAnimation.GetRandomAttackHash());
        _currentFeedbackIndex++;
    }

    public void EndStage()
    {
        _animator.SetBool(_fireDragonAnimation.DieParameterHash, true);
        StartCoroutine(Managers.Sound.VolumeDown());
    }
}

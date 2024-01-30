using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialMob : MonoBehaviour, IMonster
{
    private IPattern _pattern;
    private int _feedbackCount;
    private float _attackDelay = 28f;

    private void Awake()
    {
        _pattern = new Pattern0();
        _pattern.SetPattern();
    }

    private void Update()
    {
        if (Time.timeScale > 0)
        {
            if (_feedbackCount > 12 && Time.timeScale > 0)
            {
                _pattern.Feedback();
                _feedbackCount = 0;
            }
            if (Time.timeScale == 0)
            {
                _pattern.Pause();               //다시 시작할 때를 위해
            }
            _feedbackCount++;
        }
    }

    public (int length, float delay, BGM bgm) GetPatternData()
    {
        return (1, _attackDelay, BGM.Stage0);
    }

    public void RandomAttack()
    {
        StartCoroutine(_pattern.Attack());
    }

    public void SortPattern()
    {
        //필요없음
    }

    public void EndStage()
    {
        StartCoroutine(Managers.Sound.VolumeDown());
    }

}

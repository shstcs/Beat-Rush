using UnityEngine;

public class TutorialMob : MonoBehaviour, IMonster
{
    private IPattern _pattern;
    private int _feedbackCount;
    private float _attackDelay;

    private void Awake()
    {
        _attackDelay = 165f / (Managers.Game.noteSpeed[Managers.Game.currentStage]);
        _pattern = new IPattern(0,1);
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
            _feedbackCount++;
        }
        else
        {
            _pattern.Pause();               //다시 시작할 때를 위해
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

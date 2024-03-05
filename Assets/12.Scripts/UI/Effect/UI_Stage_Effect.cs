using UnityEngine;

public class UI_Stage_Effect : MonoBehaviour
{
    private float _time = 0;
    public float size = 0.5f;

    private float upSizeTime;
    private void Start()
    {
        int curStageidx = Managers.Game.currentStage;
        upSizeTime = (Managers.Game.stageInfos[curStageidx].bpm / Managers.Game.stageInfos[curStageidx].noteDistance) / 16;
    }

    private void Update()
    {
        if (_time <= upSizeTime)
        {
            transform.localScale = Vector3.one * (1 + size * _time);
        }
        else if (_time <= upSizeTime * 2)
        {
            transform.localScale = Vector3.one * (2 * size * upSizeTime + 1 - _time * size);
        }
        else
        {
            transform.localScale = Vector3.one;
            ResetAnim();
        }
        _time += Time.deltaTime;
    }

    public void ResetAnim()
    {
        _time = 0;
    }
}

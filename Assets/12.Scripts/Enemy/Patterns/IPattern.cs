using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IPattern
{
    private string _patternAddress;
    private List<Dictionary<string, object>> _pattern;
    private double[] _startDsp = new double[20];
    private double _pauseDsp = 0;
    private float _stageNoteSpeed;
    private float _startDelay;
    private int _curPatternNum;
    private int _curStage;
    private Vector3 _noteStartPos;
    public bool _isFeedbackStart;
    public IPattern() { }
    public IPattern(int stage, int patternNum)
    {
        _curStage = stage;
        _curPatternNum = patternNum;
        _patternAddress = $"Stage{stage}/pattern{patternNum}.csv";
        _pattern = CSVReader.Read(_patternAddress);

        _stageNoteSpeed = Managers.Game.stageInfos[stage].noteSpeed * Managers.Game.speedModifier;
        _startDelay = Managers.Game.stageInfos[stage].StageStartDelay;
        _noteStartPos = Managers.Game.stageInfos[stage].StageNotePos;
    }
    public IEnumerator Attack()
    {
        float waitTime;
        _startDsp[_curPatternNum] = AudioSettings.dspTime;

        for (int i = 0; i < _pattern.Count;)
        {
            if (i == _pattern.Count - 1)
            {
                waitTime = (128 - (float)_pattern[i]["noteLocation"]) * Managers.Game.speedModifier;
            }
            else
            {
                waitTime = ((float)_pattern[i + 1]["noteLocation"] - (float)_pattern[i]["noteLocation"]) * Managers.Game.speedModifier;
            }

            GameObject note = Managers.Pool.SpawnFromPool((float)_pattern[i]["isTrap"] != 0);
            note.GetComponent<Note>().noteNumber = _curPatternNum;
            note.GetComponent<Note>().stage = _curStage;
            note.transform.position = new Vector3((float)_pattern[i]["xValue"], 0, (Managers.Game.delay - _startDelay) * Managers.Game.speedModifier) + _noteStartPos;
            i++;
            yield return new WaitForSeconds(waitTime / _stageNoteSpeed);
        }
    }
    public void Feedback()
    {
        if (!_isFeedbackStart)
        {
            _startDsp[_curPatternNum] = AudioSettings.dspTime - _pauseDsp;
            _isFeedbackStart = true;
        }

        List<GameObject> _activeNotes = Managers.Pool.GetActiveAliveNotes(_curPatternNum);
        float _noteDistance = _stageNoteSpeed * (float)(AudioSettings.dspTime - _startDsp[_curPatternNum]) + _startDelay * Managers.Game.speedModifier;

        int cnt = 0;
        for (int i = Managers.Game.stageInfos[_curStage].curNoteInStage[_curPatternNum]; i < Managers.Game.stageInfos[_curStage].curNoteInStage[_curPatternNum] + _activeNotes.Count; i++)
        {
            float curLocation = ((float)_pattern[i]["noteLocation"] * Managers.Game.speedModifier) - _noteDistance;
            GameObject note = _activeNotes[cnt++];
            note.transform.position = new Vector3(note.transform.position.x, note.transform.position.y, curLocation + 42.5f + (Managers.Game.delay * Managers.Game.speedModifier));
        }
    }

    public void Pause()
    {

        if (_isFeedbackStart)
        {
            if (Managers.Game.Score >= 0)       //내가 이걸 왜 나눠놨을까..
            {
                _isFeedbackStart = false;
                _pauseDsp = AudioSettings.dspTime - _startDsp[_curPatternNum] + 0.03f;
            }
            else
            {
                _startDsp[_curPatternNum] = AudioSettings.dspTime;
            }
        }
    }
}

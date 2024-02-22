using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class IPattern
{
    protected List<Dictionary<string, object>> _pattern;
    protected double[] _startDsp = new double[20];
    protected double _pauseDsp = 0;
    protected float _stageNoteSpeed;
    protected float _startDelay;
    protected int _curPatternNum;
    protected int _curStage;
    protected Vector3 _noteStartPos;
    public bool _isFeedbackStart = true;

    public abstract void SetPattern();
    public virtual IEnumerator Attack()
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
            note.transform.position = new Vector3((float)_pattern[i]["xValue"], 0, (Managers.Game.delay - _startDelay) * Managers.Game.speedModifier ) + _noteStartPos;
            i++;
            yield return new WaitForSeconds(waitTime / _stageNoteSpeed);
        }
    }
    public virtual void Feedback()
    {
        if (!_isFeedbackStart)
        {
            Debug.Log("Feedback On");
            _startDsp[_curPatternNum] = AudioSettings.dspTime - _pauseDsp;
            _isFeedbackStart = true;
        }

        List<GameObject> _activeNotes = Managers.Pool.GetActiveAliveNotes(_curPatternNum);
        float _noteDistance = _stageNoteSpeed * (float)(AudioSettings.dspTime - _startDsp[_curPatternNum]) + _startDelay * Managers.Game.speedModifier;
        Debug.Log(_noteDistance);

        int cnt = 0;
        for (int i = Managers.Game.curNoteInStage[_curStage, _curPatternNum]; i < Managers.Game.curNoteInStage[_curStage, _curPatternNum] + _activeNotes.Count; i++)
        {
            float curLocation = ((float)_pattern[i]["noteLocation"] * Managers.Game.speedModifier) - _noteDistance;
            GameObject note = _activeNotes[cnt++];
            note.transform.position = new Vector3(note.transform.position.x, note.transform.position.y, curLocation + 42.5f + (Managers.Game.delay * Managers.Game.speedModifier));
        }
    }

    public virtual void Pause()
    {
        
        if (_isFeedbackStart)
        {
            if (Managers.Game.Score > 0)
            {
                _isFeedbackStart = false;
                _pauseDsp = AudioSettings.dspTime - _startDsp[_curPatternNum];
            }
            else
            {
                _startDsp[_curPatternNum] = AudioSettings.dspTime;
            }
        }
    }
}

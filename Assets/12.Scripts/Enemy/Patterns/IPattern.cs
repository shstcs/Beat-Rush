using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class IPattern
{
    protected List<Dictionary<string, object>> _pattern;
    protected double _startDsp;
    protected double _pauseDsp;
    protected float _noteSpeed;
    protected float _startDelay;
    protected int _curPatternNum;
    protected int _curStage;
    protected Vector3 _noteStartPos;
    public bool _isFeedbackStart;

    public abstract void SetPattern();
    public virtual IEnumerator Attack()
    {
        float waitTime = 0;
        _startDsp = AudioSettings.dspTime;

        for (int i = 0; i < _pattern.Count - 1;)
        {
            if (Time.timeScale != 0)
            {
                waitTime = (float)_pattern[i + 1]["noteLocation"] - (float)_pattern[i]["noteLocation"];
                GameObject note = Managers.Pool.SpawnFromPool();
                note.GetComponent<Note>().noteNumber = _curPatternNum;
                note.GetComponent<Note>().stage = _curStage;
                note.GetComponent<Note>().isTrap = (float)_pattern[i]["isTrap"] != 0;
                note.transform.position = new Vector3((float)_pattern[i]["xValue"], 0, Managers.Game.delay) + _noteStartPos;
                i++;
                yield return new WaitForSeconds(waitTime / _noteSpeed);
            }
        }
    }
    public virtual void Feedback()
    {
        if (!_isFeedbackStart)
        {
            _startDsp = AudioSettings.dspTime - _pauseDsp;
            _isFeedbackStart = true;
        }

        List<GameObject> _activeNotes = Managers.Pool.GetActiveAliveNotes(_curPatternNum);
        float _noteDistance = _noteSpeed * (float)(AudioSettings.dspTime - _startDsp) + _startDelay;

        int cnt = 0;
        for (int i = Managers.Game.curNoteInStage[_curStage, _curPatternNum]; i < Managers.Game.curNoteInStage[_curStage, _curPatternNum] + _activeNotes.Count; i++)
        {
            float curLocation = (float)_pattern[i]["noteLocation"] - _noteDistance;
            GameObject note = _activeNotes[cnt++];
            note.transform.position = new Vector3(note.transform.position.x, note.transform.position.y, curLocation + 42.5f + Managers.Game.delay);
        }
    }

    public virtual void Pause()
    {
        if (_isFeedbackStart)
        {
            _isFeedbackStart = false;
            _pauseDsp = AudioSettings.dspTime - _startDsp + 0.05f;
        }
    }
}

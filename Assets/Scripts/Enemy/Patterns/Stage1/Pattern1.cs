using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pattern1 : IPattern
{
    private List<Dictionary<string, object>> _pattern;
    private double _startDsp;
    private float _startDelay = 0.3f;
    private float _noteSpeed = 6;
    public void SetPattern()
    {
        _pattern = CSVReader.Read("Stage1/pattern1.csv");
        _startDsp = AudioSettings.dspTime;
    }

    public IEnumerator Attack(float noteSpeed)
    {
        float waitTime = 0;
        _startDsp = AudioSettings.dspTime;

        for (int i = 0; i < _pattern.Count - 1; i++)
        {
            waitTime = (float)_pattern[i + 1]["noteLocation"] - (float)_pattern[i]["noteLocation"];
            GameObject note = Managers.Pool.SpawnFromPool();
            note.transform.position = new Vector3((float)_pattern[i]["xValue"] + 40, 4, 42.5f);
            yield return new WaitForSeconds(waitTime / noteSpeed);
        }
    }

    public void FeedBack()
    {
        List<GameObject> _activeNotes = Managers.Pool.GetActiveAliveNotes();
        float _noteDistance = _noteSpeed * ((float)(AudioSettings.dspTime - _startDsp) - _startDelay);

        int cnt = 0;
        
        for (int i = 0; i < _activeNotes.Count; i++)
        {
            float curLocation = (float)_pattern[i]["noteLocation"] - _noteDistance;
            if (curLocation >= 0 && curLocation <= 32.5)
            {
                GameObject note = cnt < _activeNotes.Count ? _activeNotes[cnt] : Managers.Pool.SpawnFromPool();
                note.transform.position = new Vector3(note.transform.position.x, note.transform.position.y, curLocation + 10);
                cnt++;

            }
            else if (curLocation > 32.5) break;
        }
    }
}

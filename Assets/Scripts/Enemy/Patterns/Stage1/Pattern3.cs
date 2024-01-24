using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pattern3 : IPattern
{
    private List<Dictionary<string, object>> _pattern;

    public void SetPattern()
    {
        _pattern = CSVReader.Read("Stage1/pattern3.csv");
    }

    public IEnumerator Attack(float noteSpeed)
    {
        float waitTime = 0;

        for (int i = 0; i < _pattern.Count - 1; i++)
        {
            waitTime = (float)_pattern[i + 1]["noteLocation"] - (float)_pattern[i]["noteLocation"];
            GameObject note = Managers.Pool.SpawnFromPool();
            note.transform.position = new Vector3((float)_pattern[i]["xValue"] + 40, 4, 42.5f);
            yield return new WaitForSeconds(waitTime / noteSpeed);
        }
    }
}

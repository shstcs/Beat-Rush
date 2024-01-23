using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pattern2 : IPattern
{
    private List<Dictionary<string, object>> _pattern;

    public void SetPattern()
    {
        _pattern = CSVReader.Read("Stage1/pattern2.csv");
    }

    public IEnumerator Attack(float noteSpeed)
    {
        yield return null;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pattern1 : IPattern
{
    private List<Dictionary<string, object>> _pattern;

    public void SetPattern()
    {
        _pattern = CSVReader.Read("Stage1/pattern1.csv");
    }

    public void Attack()
    {

    }
}

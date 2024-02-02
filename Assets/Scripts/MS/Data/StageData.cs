using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class StageData
{
    public int[] MaxScoreArray = new int[4];

    public void SetData(int score)
    {
        MaxScoreArray[3] = score;
        MaxScoreArray = MaxScoreArray.OrderByDescending(n => n).ToArray();
    }
}

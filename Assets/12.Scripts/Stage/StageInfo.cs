using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageInfo
{
    public float bpm;
    public float noteDistance;
    public float noteSpeed;
    public int[] curNoteInStage = new int[20];
    public float PatternLength;
    public int PatternCount;
    public Vector3 StageNotePos;
}

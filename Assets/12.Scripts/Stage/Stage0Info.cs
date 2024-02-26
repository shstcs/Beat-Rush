using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage0Info : StageInfo
{
    public Stage0Info()
    {
        bpm = 80f;
        noteDistance = 5f;
        noteSpeed = 9.6f;
        StageStartDelay = 6.6666f;
        PatternLength = 165f;
        PatternCount = 1;
        StageNotePos = new Vector3(-2, 0, 42.5f);
    }
}

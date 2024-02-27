using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage0Info : StageInfo
{
    public Stage0Info()
    {
        bpm = 80f;
        noteDistance = 5f;
        noteSpeed = 6.6666f;
        StageStartDelay = 0f;
        PatternLength = 80f;
        PatternCount = 5;
        StageNotePos = new Vector3(-2, 0, 42.5f);
    }
}

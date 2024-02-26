using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage2Info : StageInfo
{
    public Stage2Info()
    {
        bpm = 99f;
        noteDistance = 8f;
        noteSpeed = 13.2f;
        StageStartDelay = -2.5f;
        PatternLength = 128f;
        PatternCount = 9;
        StageNotePos = new Vector3(40, 1, 42.5f);
    }
}

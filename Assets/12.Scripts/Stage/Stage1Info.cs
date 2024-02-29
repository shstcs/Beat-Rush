using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage1Info : StageInfo
{
    public Stage1Info()
    {
        bpm = 72f;
        noteDistance = 8f;
        noteSpeed = 9.6f;
        PatternLength = 320f;
        PatternCount = 4;
        StageNotePos = new Vector3(40, 1, 42.5f);
    }
}

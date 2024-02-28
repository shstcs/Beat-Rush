using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage3Info : StageInfo
{
    public Stage3Info()
    {
        bpm = 100f;
        noteDistance = 8f;
        noteSpeed = 13.3333f;
        StageStartDelay = 0f;
        PatternLength = 128f;
        PatternCount = 17;
        StageNotePos = new Vector3(40, 1, 42.5f);
    }
}

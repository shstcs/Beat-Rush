using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pattern2_8 : IPattern
{
    public override void SetPattern()
    {
        _pattern = CSVReader.Read("Stage2/pattern8.csv");
        _noteSpeed = 13.2f;
        _noteStartPos = Managers.Game.StageNotePos[2];
        _curPatternNum = 8;
        _curStage = 2;
    }

    public override void Feedback()
    {
        _startDelay = Managers.Game.StageStartDelay[2];
        base.Feedback();
    }
}

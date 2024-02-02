using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pattern1_1 : IPattern
{
    public override void SetPattern()
    {
        _pattern = CSVReader.Read("Stage1/pattern1.csv");
        _noteSpeed = 6;
        _noteStartPos = Managers.Game.StageNotePos[1];
        _curPatternNum = 1;
        _curStage = 1;
    }

    public override void Feedback()
    {
        _startDelay = Managers.Game.StageStartDelay[1];
        base.Feedback();
    }
}

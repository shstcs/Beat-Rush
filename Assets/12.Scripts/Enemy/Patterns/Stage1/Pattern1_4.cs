using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pattern1_4 : IPattern
{
    public override void SetPattern()
    {
        _pattern = CSVReader.Read("Stage1/pattern4.csv");
        _stageNoteSpeed = Managers.Game.noteSpeed[Managers.Game.currentStage] * Managers.Game.speedModifier;
        _noteStartPos = Managers.Game.StageNotePos[1];
        _curPatternNum = 4;
        _curStage = 1;
    }

    public override void Feedback()
    {
        _startDelay = Managers.Game.StageStartDelay[1];
        base.Feedback();
    }
}

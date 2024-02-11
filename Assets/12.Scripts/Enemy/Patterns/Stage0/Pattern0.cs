using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pattern0 : IPattern
{
    public override void SetPattern()
    {
        _pattern = CSVReader.Read("Tutorial/tutorial.csv");
        _stageNoteSpeed = Managers.Game.noteSpeed[Managers.Game.currentStage] * Managers.Game.speedModifier;
        _noteStartPos = Managers.Game.StageNotePos[0];
        _curPatternNum = 0;
        _curStage = 0;
    }

    public override void Feedback()
    {
        _startDelay = Managers.Game.StageStartDelay[0];
        base.Feedback();
    }

}

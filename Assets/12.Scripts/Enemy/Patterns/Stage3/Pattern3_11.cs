using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pattern3_11 : IPattern
{
    public override void SetPattern()
    {
        _pattern = CSVReader.Read("Stage3/pattern11.csv");
        _stageNoteSpeed = Managers.Game.noteSpeed[Managers.Game.currentStage] * Managers.Game.speedModifier;
        _noteStartPos = Managers.Game.StageNotePos[3];
        _curPatternNum = 11;
        _curStage = 3;
    }

    public override void Feedback()
    {
        _startDelay = Managers.Game.StageStartDelay[3];
        base.Feedback();
    }
}

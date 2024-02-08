using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pattern1_3 : IPattern
{
    public override void SetPattern()
    {
        _pattern = CSVReader.Read("Stage1/pattern3.csv");
        _noteSpeed = Managers.Game.noteDistance[Managers.Game.currentStage] / (60 / Managers.Game.bpm[Managers.Game.currentStage]);
        _noteStartPos = Managers.Game.StageNotePos[1];
        _curPatternNum = 3;
        _curStage = 1;
    }

    public override void Feedback()
    {
        _startDelay = Managers.Game.StageStartDelay[1];
        base.Feedback();
    }
}

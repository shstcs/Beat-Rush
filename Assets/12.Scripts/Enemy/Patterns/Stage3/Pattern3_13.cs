using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pattern3_13 : IPattern
{
    public override void SetPattern()
    {
        _pattern = CSVReader.Read("Stage3/pattern13.csv");
        _noteSpeed = Managers.Game.noteDistance[Managers.Game.currentStage] / (60 / Managers.Game.bpm[Managers.Game.currentStage]);
        _noteStartPos = Managers.Game.StageNotePos[3];
        _curPatternNum = 13;
        _curStage = 3;
    }

    public override void Feedback()
    {
        _startDelay = Managers.Game.StageStartDelay[3];
        base.Feedback();
    }
}

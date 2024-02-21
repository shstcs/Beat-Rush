using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Popup_StageWanring : MonoBehaviour, IPopup
{
    private void Start()
    {
        Managers.Sound.StopBGM();
        Managers.Popup.CurrentPopup = this;
    }

    private void OnDisable()
    {
        Time.timeScale = 1.0f;
        Managers.Sound.DelayedPlayBGM(BGM.Stage3, 32.5f / (Managers.Game.noteSpeed[3] * Managers.Game.speedModifier));        //음악 재생
        Managers.Popup.CurrentPopup = null;
    }

    public void OffPopup()
    {

    }
}

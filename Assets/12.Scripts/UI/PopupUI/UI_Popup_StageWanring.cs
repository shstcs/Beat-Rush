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
        Managers.Game.CallStageStart();
        Managers.Popup.CurrentPopup = null;
    }

    public void OffPopup()
    {

    }
}

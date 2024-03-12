using UnityEngine;

public class UI_Popup_Tutorial : MonoBehaviour, IPopup
{
    private void Start()
    {
        Managers.Sound.StopBGM();
        Managers.Popup.CurrentPopup = this;
    }

    private void OnDisable()
    {
        Managers.Sound.DelayedPlayBGM(0, 32.5f / (Managers.Game.stageInfos[0].noteSpeed * Managers.Game.speedModifier));        //음악 재생
        Managers.Popup.CurrentPopup = null;
    }

    public void OffPopup()
    {

    }
}

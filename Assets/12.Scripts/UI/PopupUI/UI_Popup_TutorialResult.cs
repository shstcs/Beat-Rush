using UnityEngine;

public class UI_Popup_TutorialResult : MonoBehaviour, IPopup
{
    private void OnEnable()
    {
        Managers.Popup.CurrentPopup = this;
    }
    private void OnDisable()
    {
        Managers.Game.InitJudge();
        Managers.Popup.CurrentPopup = null;
    }

    public void OffPopup()
    {

    }
}

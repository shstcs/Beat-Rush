using UnityEngine;

public class UI_Popup_Timer : MonoBehaviour, IPopup
{
    private void OnEnable()
    {
        Managers.Popup.CurrentPopup = this;
    }

    private void OnDisable()
    {
        Managers.Popup.CurrentPopup = null;
    }

    public void OffPopup()
    {

    }

}

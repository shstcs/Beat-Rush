using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Popup_Quest : MonoBehaviour, IPopup
{
    private void OnEnable()
    {
        Managers.Popup.CurrentPopup = this;
    }

    public void OffPopup()
    {
        gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        Managers.Popup.CurrentPopup = null;
        QuestManager.instance.QuestNotice();
    }
}

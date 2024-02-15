using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI_Popup_TutorialResult : MonoBehaviour, IPopup
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

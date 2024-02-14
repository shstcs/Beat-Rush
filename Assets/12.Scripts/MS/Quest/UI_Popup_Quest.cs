using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Popup_Quest : MonoBehaviour
{
    private void OnEnable()
    {
        Managers.Game.IsLobbyPopup = true;
    }

    public void closeWindow()
    {
        gameObject.SetActive(false);

        Managers.Game.IsLobbyPopup = false;
    }
}

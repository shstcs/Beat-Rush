using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI_Popup_Tutorialpanel : MonoBehaviour
{
    private void OnEnable()
    {
        Managers.Game.IsLobbyPopup = true;
    }
    public void TutorialBtn()
    {
        Managers.Game.PlayerSpwanPosition = Managers.Player.transform.position;
        Managers.Game.PlayerSpwanRotation = Managers.Player.transform.rotation;
        SceneManager.LoadScene("Tutorial");
    }
    private void OnDisable()
    {
        Managers.Game.IsLobbyPopup = false;
    }
}
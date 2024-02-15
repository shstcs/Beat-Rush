using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI_Popup_Tutorialpanel : MonoBehaviour, IPopup
{
    private void OnEnable()
    {
        Managers.Popup.CurrentPopup = this;
    }
    public void TutorialBtn()
    {
        Managers.Game.PlayerSpwanPosition = Managers.Player.transform.position;
        Managers.Game.PlayerSpwanRotation = Managers.Player.transform.rotation;
        SceneManager.LoadScene("Tutorial");
    }
    private void OnDisable()
    {
        Managers.Popup.CurrentPopup = null;
    }

    public void OffPopup()
    {
        Managers.Popup.CurrentPopup = null;
        gameObject.SetActive(false);
    }
}
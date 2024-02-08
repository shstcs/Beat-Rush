using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Popup_Tutorial : MonoBehaviour
{
    private void Start()
    {
        Managers.Game.lockType = InputLockType.Lock;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        Time.timeScale = 0.0f;
    }

    private void OnDisable()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        Time.timeScale = 1.0f;
        Managers.Game.lockType = InputLockType.UnLock;
    }
}

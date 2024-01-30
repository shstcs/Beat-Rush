using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class UI_Popup_Option : MonoBehaviour
{
    private void OnEnable()
    {
        //Time.timeScale = 0.0f;
        Cursor.lockState = CursorLockMode.None;
        Managers.Game.lockType = InputLockType.Lock;
        Cursor.visible = true;
    }
    private void Update()
    {
        //옵션 창 닫는 부분은 나중에 Input System으로 처리해도 될 것 같습니다.
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            OffOption();
        }
    }
    public void OffOption()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        Time.timeScale = 1.0f;
        Managers.Game.lockType = InputLockType.UnLock;
        gameObject.SetActive(false);
        Managers.Sound.ContinueBGM();        //음악 재생
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class UI_Popup_Option : MonoBehaviour, IPopup
{
    private void OnEnable()
    {
        Managers.Popup.CurrentPopup = this;
        Managers.Player.Input.PlayerActions.Popup.started += OffOption;
    }

    //private void Update()
    //{
    //    //옵션 창 닫는 부분은 나중에 Input System으로 처리해도 될 것 같습니다.
    //    if (Input.GetKeyDown(KeyCode.Escape))
    //    {
    //        OffOption();
    //    }
    //}

    private void OnDisable()
    {
        Managers.Player.Input.PlayerActions.Popup.started -= OffOption;
    }

    public void OnLobby()
    {
        OffWindow();
        if (SceneManager.GetActiveScene().name == "Lobby")
            return;
        Managers.Sound.StopBGM();
        Managers.Sound.LoopPlayBGM(BGM.Lobby2);
        SceneManager.LoadScene("Lobby");
    }
    public void OffOption(InputAction.CallbackContext context)
    {
        OffWindow();
    }

    private void OffWindow()
    {
        Managers.Popup.CurrentPopup = null;
        Managers.Sound.ContinueBGM();        //음악 재생
        gameObject.SetActive(false);
    }

    public void ExitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Apllication.Quit();
#endif
    }

    public void OffPopup()
    {
    
    }
}

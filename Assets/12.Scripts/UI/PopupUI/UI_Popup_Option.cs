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

    private void OnDisable()
    {
        Managers.Player.Input.PlayerActions.Popup.started -= OffOption;
    }

    public void OnLobby()
    {
        if (Managers.Data.CurrentStateData.CurrentClearStage < 1) return;

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

        if (Managers.Game.GameType == GameType.Play)
        {
            Managers.Game.CallContinue();
        }
    }

    private void OffWindow()
    {
        Managers.Popup.CurrentPopup = null;
        //Managers.Sound.ContinueBGM();        //음악 재생
        gameObject.SetActive(false);
    }


    public void ExitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#elif UNITY_WEBGL
        SceneManager.LoadScene
#else
        Application.Quit();
#endif
    }

    public void OffPopup()
    {
    
    }
}

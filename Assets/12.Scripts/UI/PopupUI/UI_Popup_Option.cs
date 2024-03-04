using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class UI_Popup_Option : MonoBehaviour, IPopup
{
    private float _curDelay;
    private void OnEnable()
    {
        Managers.Popup.CurrentPopup = this;
        Managers.Player.Input.PlayerActions.Popup.started += OffOption;
        _curDelay = Managers.Game.delay;
    }

    private void OnDisable()
    {
        if (Managers.Player != null)
            Managers.Player.Input.PlayerActions.Popup.started -= OffOption;
        if (_curDelay != Managers.Game.delay)
            Managers.Data.SavePlayerData();
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
        SceneManager.LoadScene("Start");
#else
        Application.Quit();
#endif
    }

    public void OffPopup()
    {

    }
}

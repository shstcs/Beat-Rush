using UnityEngine;
using UnityEngine.InputSystem;

public class UI_Scene_Tutorial : MonoBehaviour
{
    private void Awake()
    {
        Managers.UI.SetUI();
    }
    private void Start()
    {
        GameObject.Find("JudgeCanvas").transform.GetChild(1).gameObject.SetActive(true);
        Managers.Player.Input.PlayerActions.Popup.started += OnOption;
    }

    private void OnOption(InputAction.CallbackContext context)
    {
        if (Managers.Popup.IsPopupActive()) return;

        Managers.Sound.PauseBGM();           //노래 정지
        GameObject.Find("HUD_Canvas").transform.GetChild(2).gameObject.SetActive(true);
    }

    private void OnDisable()
    {
        Managers.Player.Input.PlayerActions.Popup.started -= OnOption;
    }

    private void OnApplicationFocus(bool focus)
    {
        if (!focus)
        {
            if (Managers.Popup.IsPopupActive()) return;

            Managers.Sound.PauseBGM();           //노래 정지
            GameObject.Find("HUD_Canvas").transform.GetChild(2).gameObject.SetActive(true);
        }
    }
}
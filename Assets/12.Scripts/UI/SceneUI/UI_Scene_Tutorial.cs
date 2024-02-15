using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class UI_Scene_Tutorial : MonoBehaviour
{
    private void Awake()
    {
        Managers.UI.SetUI();
        //Managers.Game.GetKeyDown += OnOption;
    }
    private void Start()
    {
        GameObject.Find("JudgeCanvas").transform.GetChild(1).gameObject.SetActive(true);
        Managers.Player.Input.PlayerActions.Popup.started += OnOption;
    }

    private void Update()
    {
        //옵션 창 여는 부분은 나중에 Input System으로 처리해도 될 것 같습니다.
        //if (Input.GetKeyDown(KeyCode.Escape))
        //{
        //    Managers.Game.GetKeyDown?.Invoke();
        //}
    }

    private void OnOption(InputAction.CallbackContext context)
    {
        if (Managers.Popup.IsPopupActive()) return;

        Managers.Sound.PauseBGM();           //노래 정지
        GameObject.Find("HUD_Canvas").transform.GetChild(2).gameObject.SetActive(true);
    }

    private void OnDisable()
    {
        // Managers.Game.GetKeyDown -= OnOption;
        Managers.Player.Input.PlayerActions.Popup.started -= OnOption;
    }
}
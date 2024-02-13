using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer.Internal;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI_Scene_Lobby : MonoBehaviour
{
    private void Awake()
    {
        Managers.Game.GameType = GameType.Lobby;
    }
    private void Start()
    {
        Time.timeScale = 1.0f;
        Managers.UI.SetUI();
        Managers.Game.GetKeyDown += OnOption;
        Managers.Sound.LoopPlayBGM(BGM.Lobby2);
    }
    private void Update()
    {
        //옵션 창 여는 부분은 나중에 Input System으로 처리해도 될 것 같습니다.
        if (Input.GetKeyDown(KeyCode.Escape) && !Managers.Game.IsLobbyPopup)
            Managers.Game.GetKeyDown?.Invoke();
    }

    private void OnOption()
    {
        GameObject.Find("HUD_Canvas").transform.GetChild(0).gameObject.SetActive(true);
    }
    private void OnDisable()
    {
        Managers.Game.GetKeyDown -= OnOption;
    }
}

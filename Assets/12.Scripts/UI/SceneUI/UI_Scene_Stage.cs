using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class UI_Scene_Stage : MonoBehaviour
{
    private void Start()
    {
        Time.timeScale = 1.0f;
        Managers.Game.Score = 0;
        Managers.Game.Combo = 0;
        Managers.Game.MaxCombo = 0;
        Managers.UI.SetUI();
        Managers.Game.GetKeyDown += OnOption;
        Managers.Game.OnStageEnd += OnStageEnd;

        Debug.Log(Managers.Game.mode.ToString());
    }

    private void Update()
    {
        //옵션 창 여는 부분은 나중에 Input System으로 처리해도 될 것 같습니다.
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Managers.Game.GetKeyDown?.Invoke();
        }
    }

    private void OnOption()
    {
        if (Managers.Game.IsLobbyPopup) return;

        Managers.Sound.PauseBGM();           //노래 정지
        GameObject.Find("Canvas").transform.GetChild(0).gameObject.SetActive(true);
    }

    private void OnStageEnd()
    {
        GameObject.Find("Canvas").transform.GetChild(4).gameObject.SetActive(true);
    }

    private void OnDisable()
    {
        Managers.Game.GetKeyDown -= OnOption;
    }

    private void OnApplicationFocus(bool focus)
    {
        if (!focus)
        {
            OnOption();
        }
    }

}

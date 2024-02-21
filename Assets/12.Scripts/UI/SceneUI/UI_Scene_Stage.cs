using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class UI_Scene_Stage : MonoBehaviour
{
    private void Start()
    {
        Managers.Player.Input.PlayerActions.Popup.started += OnOption;
        Time.timeScale = 1.0f;
        Managers.Game.Score = 0;
        Managers.Game.Combo = 0;
        Managers.Game.MaxCombo = 0;
        Managers.UI.SetUI();
        Managers.Game.OnStageEnd += OnStageEnd;

        Debug.Log(Managers.Game.mode.ToString());
    }

    private void OnOption(InputAction.CallbackContext context)
    {
        if (Managers.Popup.IsPopupActive()) return;

        Managers.Sound.PauseBGM();           //노래 정지
        GameObject.Find("Canvas").transform.GetChild(0).gameObject.SetActive(true);
    }

    private void OnStageEnd()
    {
        GameObject.Find("Canvas").transform.GetChild(4).gameObject.SetActive(true);
        if (!Managers.Player.IsUseSkill)
            QuestManager.instance.SetQuestClear(QuestName.NoSkillStageClear);
    }

    private void OnDisable()
    {
        Managers.Player.Input.PlayerActions.Popup.started -= OnOption;
        Managers.Game.OnStageEnd -= OnStageEnd;
    }

    private void OnApplicationFocus(bool focus)
    {
        if (!focus)
        {
            if (Managers.Popup.IsPopupActive()) return;

            Managers.Sound.PauseBGM();           //노래 정지
            GameObject.Find("Canvas").transform.GetChild(0).gameObject.SetActive(true);
        }
    }

}

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_HUD_Stage : MonoBehaviour
{
    private float _hpBarMax;
    private float _skillBarMax;
    private GameObject HUD_Combo;
    private GameObject HUD_PlayerInfo;
    private GameObject HUD_Score;
    private void Start()
    {
        _hpBarMax = Managers.Data.CurrentStateData.GetHealth();
        _skillBarMax = 100.0f;
        Managers.Data.CurrentStateData.CurrentHealth = (int)_hpBarMax;
        Managers.Data.CurrentStateData.SkillGauge = 0;
        HUD_Combo = GameObject.Find("HUD_Combo");
        HUD_PlayerInfo = GameObject.Find("HUD_PlayerInfo");
        HUD_Score = GameObject.Find("HUD_Score");
        HUD_PlayerInfo.transform.GetChild(0).GetComponent<Image>().fillAmount = 1.0f;
        HUD_PlayerInfo.transform.GetChild(1).GetComponent<Image>().fillAmount = 0.0f;
    }

    private void Update()
    {
        HUD_PlayerInfo.transform.GetChild(0).GetComponent<Image>().fillAmount = Managers.Data.CurrentStateData.CurrentHealth / _hpBarMax;
        HUD_PlayerInfo.transform.GetChild(1).GetComponent<Image>().fillAmount = Managers.Data.CurrentStateData.SkillGauge / _skillBarMax;
        HUD_Combo.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = Managers.Game.Combo.ToString();
        HUD_Combo.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = Managers.Game.curJudge;
        HUD_Score.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = Managers.Game.Score.ToString();
        HUD_Score.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text = Managers.Game.rank.ToString();
    }
}
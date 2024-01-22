using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_HUD_Stage : MonoBehaviour
{
    private float _hpBarMax;
    private float _skillBarMax;
    private void Start()
    {
        _hpBarMax = 10.0f;
        _skillBarMax = 100.0f;
        Managers.Player.CurrentStateData.Health = (int)_hpBarMax;
        Managers.Player.CurrentStateData.SkillGauge = 0;
        GameObject.Find("HUD_PlayerInfo").transform.GetChild(0).GetComponent<Image>().fillAmount = 1.0f;
        GameObject.Find("HUD_PlayerInfo").transform.GetChild(1).GetComponent<Image>().fillAmount = 0.0f;
    }

    private void Update()
    {
        GameObject.Find("HUD_PlayerInfo").transform.GetChild(0).GetComponent<Image>().fillAmount = Managers.Player.CurrentStateData.Health / _hpBarMax;
        GameObject.Find("HUD_PlayerInfo").transform.GetChild(1).GetComponent<Image>().fillAmount = Managers.Player.CurrentStateData.SkillGauge / _skillBarMax;
        GameObject.Find("HUD_Combo").transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = Managers.Game.Combo.ToString();
        GameObject.Find("HUD_Score").transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = Managers.Game.Score.ToString();
    }
}
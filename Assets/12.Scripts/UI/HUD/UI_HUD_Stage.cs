using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_HUD_Stage : MonoBehaviour
{
    private float _hpBarMax;
    private float _skillBarMax;
    private GameObject HUD_PlayerInfo;
    private GameObject HUD_Score;
    private Image damagePanel;
    private Coroutine damaged;
    private void Start()
    {
        _hpBarMax = Managers.Data.CurrentStateData.GetHealth();
        _skillBarMax = 100.0f;
        Managers.Data.CurrentStateData.CurrentHealth = (int)_hpBarMax;
        Managers.Data.CurrentStateData.SkillGauge = 0;
        HUD_PlayerInfo = gameObject.transform.GetChild(0).GetChild(1).gameObject;
        HUD_Score = gameObject.transform.GetChild(0).GetChild(2).gameObject;
        damagePanel = gameObject.transform.GetChild(0).GetChild(4).GetComponent<Image>();
        HUD_PlayerInfo.transform.GetChild(0).GetChild(0).GetComponent<Image>().fillAmount = 1.0f;
        HUD_PlayerInfo.transform.GetChild(1).GetChild(0).GetComponent<Image>().fillAmount = 0.0f;
        Managers.Game.OnDamaged += DamagePanel;

        if (Managers.Game.currentStage == 3)
            gameObject.transform.GetChild(0).GetChild(5).gameObject.SetActive(true);
    }

    private void Update()
    {
        HUD_PlayerInfo.transform.GetChild(0).GetChild(0).GetComponent<Image>().fillAmount = Managers.Data.CurrentStateData.CurrentHealth / _hpBarMax;
        HUD_PlayerInfo.transform.GetChild(1).GetChild(0).GetComponent<Image>().fillAmount = Managers.Data.CurrentStateData.SkillGauge / _skillBarMax;
        HUD_Score.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = Managers.Game.Score.ToString();
        HUD_Score.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text = Managers.Game.rank.ToString();
    }
    private void DamagePanel()
    {
        if (damaged != null)
        {
            StopCoroutine(damaged);
        }
        if (Managers.Player.IsDie())
            damagePanel.gameObject.SetActive(false);

        damagePanel.enabled = true;
        damagePanel.color = Color.red;
        damaged = StartCoroutine(OnDamagePanel());
    }
    private IEnumerator OnDamagePanel()
    {
        float startAlpha = 0.3f;
        float a = startAlpha;

        while (a > 0.0f)
        {
            a -= (startAlpha / 0.3f) * Time.deltaTime;
            damagePanel.color = new Color(1.0f, 0.0f, 0.0f, a);
            yield return null;
        }

        damagePanel.enabled = false;
    }

    private void OnDisable()
    {
        StopAllCoroutines();
        Managers.Game.OnDamaged -= DamagePanel;
    }
}
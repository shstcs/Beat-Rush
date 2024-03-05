using TMPro;
using UnityEngine;

public class UI_Popup_Stats : MonoBehaviour, IPopup
{
    [SerializeField] private TextMeshProUGUI _healthText;
    [SerializeField] private TextMeshProUGUI _skillDistance;
    [SerializeField] private TextMeshProUGUI _skillGaugeIncrement;
    [SerializeField] private TextMeshProUGUI _skillSpeed;
    [SerializeField] private TextMeshProUGUI _moveSpeed;

    private void OnEnable()
    {
        Managers.Popup.CurrentPopup = this;

        _healthText.text = Managers.Data.CurrentStateData.GetHealth().ToString();
        _skillDistance.text = Managers.Data.CurrentSkillData.GetDistance().ToString();
        _skillGaugeIncrement.text = new string($"+{((Managers.Data.CurrentStateData.SkillGaugeModifier - 1) * 100).ToString()}%");
        _skillSpeed.text = Managers.Data.CurrentSkillData.GetSpeed().ToString();
        _moveSpeed.text = new string($"+{((Managers.Data.CurrentStateData.SpeedModifier - 1) * 100).ToString()}%");
    }

    public void OffPopup()
    {
        gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        Managers.Popup.CurrentPopup = null;
    }
}

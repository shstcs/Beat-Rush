using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UILobbyExpBar : MonoBehaviour
{
    [SerializeField] private Image expImage;
    [SerializeField] private TextMeshProUGUI text;

    private int maxExp = 10;
    private int currentExp;

    private void Start()
    {
        CheckExp();
    }

    public void CheckExp()
    {
        currentExp = Managers.Data.CurrentStateData.Exp;
        text.text = currentExp.ToString() + " / " + maxExp.ToString();

        expImage.fillAmount = (float)currentExp / maxExp;
    }
}

using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using TMPro;
using UnityEngine;

public class UI_Judge_Effect : MonoBehaviour
{
    private GameObject ComboText;
    private GameObject JudgeText;

    private void Start()
    {
        JudgeText = gameObject.transform.GetChild(0).gameObject;
        ComboText = gameObject.transform.GetChild(1).gameObject;
        JudgeText.GetComponent<TextMeshProUGUI>().text = " ".ToString();
        ComboText.GetComponent<TextMeshProUGUI>().text = " ".ToString();
    }
    private void Update()
    {
        if (Managers.Game.Combo > 2)
            ComboText.GetComponent<TextMeshProUGUI>().text = Managers.Game.Combo.ToString() + " Combo!";
        JudgeText.GetComponent<TextMeshProUGUI>().text = Managers.Game.curJudge;
    }
}

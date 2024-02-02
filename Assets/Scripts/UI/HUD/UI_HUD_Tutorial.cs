using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UI_HUD_Tutorial : MonoBehaviour
{
    private void Update()
    {
        gameObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = Managers.Game.delay.ToString();
    }
}

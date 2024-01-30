using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UILobbyLevel : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text;

    private void Start()
    {
        CheckLevel();
    }

    public void CheckLevel()
    {
        text.text = Managers.Player.CurrentStateData.Level.ToString();
    }
}

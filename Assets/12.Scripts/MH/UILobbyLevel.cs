using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UILobbyLevel : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text;

    private void Start()
    {
        Managers.Game.OnLevel += CheckLevel;
    }

    private void OnDestroy()
    {
        Managers.Game.OnLevel -= CheckLevel;
    }

    public void CheckLevel()
    {
        text.text = Managers.Data.CurrentStateData.Level.ToString();
    }
}

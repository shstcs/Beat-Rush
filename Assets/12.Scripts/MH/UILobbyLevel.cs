using TMPro;
using UnityEngine;

public class UILobbyLevel : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text;

    private void Start()
    {
        CheckLevel();
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

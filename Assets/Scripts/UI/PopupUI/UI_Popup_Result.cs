using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI_Popup_Result : MonoBehaviour
{
    private void OnEnable()
    {
        Time.timeScale = 0.0f;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    private void Start()
    {
        if (Managers.Player.IsDie() == false)
            ClearResult();
        else
            return;
    }
    public void LoadStage()
    {
        Managers.Game.Score = 0;
        Managers.Game.Combo = 0;
        Managers.Game.MaxCombo = 0;
        SceneManager.LoadScene("Minho");
    }

    public void LoadLobby()
    {
        SceneManager.LoadScene("Lobby");
    }

    private void ClearResult()
    {
        GameObject.Find("Score_Text").transform.GetComponent<TextMeshProUGUI>().text = Managers.Game.Score.ToString();
        GameObject.Find("Combo_Text").transform.GetComponent<TextMeshProUGUI>().text = Managers.Game.MaxCombo.ToString();
    }
}

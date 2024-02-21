using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class UI_Popup_Result : MonoBehaviour, IPopup
{
    private void OnEnable()
    {
       Managers.Popup.CurrentPopup = this;
    }

    private void Start()
    {
        if (Managers.Game.Score > Managers.Data.BestScore)
        {
            PlayerPrefs.SetInt("BestScore", Managers.Game.Score);
            gameObject.transform.GetChild(12).gameObject.SetActive(true);
        }

        if (Managers.Player.IsDie() == true)
            transform.GetChild(0).transform.GetComponent<TextMeshProUGUI>().text = "패배...";
        else
            transform.GetChild(0).transform.GetComponent<TextMeshProUGUI>().text = "승리!";

        ClearResult();
    }
    public void LoadStage()
    {
        Managers.Game.Score = 0;
        Managers.Game.Combo = 0;
        Managers.Game.MaxCombo = 0;
        Managers.Game.InitJudge();
        Managers.Game.InitNotes();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void LoadLobby()
    {
        Managers.Game.InitJudge();
        SceneManager.LoadScene("Lobby");
    }

    private void ClearResult()
    {
        Managers.Game.SetRank();
        if (Managers.Game.rank == Rank.S)
            QuestManager.instance.SetQuestClear(QuestName.SRankClear);
        gameObject.transform.GetChild(1).transform.GetComponent<TextMeshProUGUI>().text = Managers.Game.Score.ToString();
        gameObject.transform.GetChild(2).transform.GetComponent<TextMeshProUGUI>().text = Managers.Game.rank.ToString();
        gameObject.transform.GetChild(3).transform.GetComponent<TextMeshProUGUI>().text = Managers.Game.MaxCombo.ToString();
        gameObject.transform.GetChild(4).transform.GetComponent<TextMeshProUGUI>().text = Managers.Game.judgeNotes[(int)Score.Perfect].ToString();
        gameObject.transform.GetChild(5).transform.GetComponent<TextMeshProUGUI>().text = Managers.Game.judgeNotes[(int)Score.Great].ToString();
        gameObject.transform.GetChild(6).transform.GetComponent<TextMeshProUGUI>().text = Managers.Game.judgeNotes[(int)Score.Good].ToString();
        gameObject.transform.GetChild(7).transform.GetComponent<TextMeshProUGUI>().text = Managers.Game.judgeNotes[(int)Score.Bad].ToString();
        gameObject.transform.GetChild(8).transform.GetComponent<TextMeshProUGUI>().text = Managers.Game.judgeNotes[(int)Score.Miss].ToString();
        if (Managers.Game.judgeNotes[3] == 0 && Managers.Game.judgeNotes[4] == 0)
        {
            gameObject.transform.GetChild(11).gameObject.SetActive(true);
        }
    }

    private void OnDisable()
    {
        Managers.Popup.CurrentPopup = null;
    }

    public void OffPopup()
    {
        
    }
}

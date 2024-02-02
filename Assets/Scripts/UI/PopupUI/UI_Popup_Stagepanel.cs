using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UI_Popup_Stagepanel : MonoBehaviour
{
    private string currentStageName;
    private void OnEnable()
    {
        Time.timeScale = 0.0f;
        Cursor.lockState = CursorLockMode.None;
        Managers.Game.lockType = InputLockType.Lock;
        Cursor.visible = true;
        SetText();
    }
    //private void Update()
    //{
    //    if (Managers.Game.currentStage > 0)
    //        currentStageName = "스테이지 " + Managers.Game.currentStage.ToString();
    //    else
    //        currentStageName = "판정 보정";
    //    gameObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = currentStageName;
    //    gameObject.transform.GetChild(2).transform.GetChild(0).transform.GetComponent<TextMeshProUGUI>().text = Managers.Data.BestScore.ToString();
    //}

    private void OnDisable()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        Time.timeScale = 1.0f;
        Managers.Game.lockType = InputLockType.UnLock;
    }

    private void SetText()
    {
        if (Managers.Game.currentStage > 0)
            currentStageName = "스테이지 " + Managers.Game.currentStage.ToString();
        else
            currentStageName = "판정 보정";
        gameObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = currentStageName;

        var scoreArray = Managers.Game.MaxScoreArray[Managers.Game.currentStage].MaxScoreArray;
        gameObject.transform.GetChild(2).transform.GetChild(0).transform.GetComponent<TextMeshProUGUI>().text = scoreArray[0].ToString();
        gameObject.transform.GetChild(2).transform.GetChild(1).transform.GetComponent<TextMeshProUGUI>().text = scoreArray[1].ToString();
        gameObject.transform.GetChild(2).transform.GetChild(2).transform.GetComponent<TextMeshProUGUI>().text = scoreArray[2].ToString();

    }
}
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UI_Popup_Stagepanel : MonoBehaviour
{
    private Image monsterImage;
    private string currentStageName;
    private void OnEnable()
    {
        //Time.timeScale = 0.0f;
        //Cursor.lockState = CursorLockMode.None;
        //Managers.Game.lockType = InputLockType.Lock;
        //Cursor.visible = true;
        Managers.Game.LobbyPopupCount++;
        SetText();
        SetImage();
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
        Managers.Game.LobbyPopupCount = 0;
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

    private void SetImage()
    {
        monsterImage = transform.GetChild(1).GetComponent<Image>();
        if (Managers.Game.currentStage == 1)
        {
            monsterImage.sprite = Resources.Load<Sprite>("Stage1Image");
        }
        else if (Managers.Game.currentStage == 2)
        {
            monsterImage.sprite = Resources.Load<Sprite>("Stage2Image");
        }
        else if (Managers.Game.currentStage == 3)
        {
            monsterImage.sprite = Resources.Load<Sprite>("Stage3Image");
        }
    }
    public void StartStage()
    {
        Managers.Game.PlayerSpwanPosition = Managers.Player.transform.position;
        Managers.Game.PlayerSpwanRotation = Managers.Player.transform.rotation;
        Debug.Log(Managers.Game.PlayerSpwanPosition);

        switch (Managers.Game.currentStage)
        {
            case 0:
                SceneManager.LoadScene("Tutorial");
                break;
            case 1:
                Debug.Log(Managers.Data.CurrentStateData.CurrentClearStage);
                if (Managers.Data.CurrentStateData.CurrentClearStage >= 1)
                {
                    SceneManager.LoadScene("Stage_1");
                }
                else
                {
                    PopupAlert();
                }
                break;
            case 2:
                Debug.Log(Managers.Data.CurrentStateData.CurrentClearStage);
                if (Managers.Data.CurrentStateData.CurrentClearStage >= 2)
                {
                    SceneManager.LoadScene("Stage_2");
                }
                else
                {
                    PopupAlert();
                }
                break;
            case 3:
                Debug.Log(Managers.Data.CurrentStateData.CurrentClearStage);
                if (Managers.Data.CurrentStateData.CurrentClearStage >= 0) // 테스트를 위해
                {
                    Managers.Game.LobbyPopupCount = 0;
                    SceneManager.LoadScene("Stage_3");
                }
                else
                {
                    PopupAlert();
                }
                break;
        }
    }

    public void PopupAlert()
    {
        gameObject.transform.GetChild(5).gameObject.SetActive(true);
    }
}
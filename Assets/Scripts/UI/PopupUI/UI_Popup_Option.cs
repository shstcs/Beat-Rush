using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI_Popup_Option : MonoBehaviour
{
    public void LoadStage()
    {
        SceneManager.LoadScene("StageUI_Test_Scene");
    }

    public void LoadStart()
    {
        SceneManager.LoadScene("StartUI_Test_Scene");
    }
}

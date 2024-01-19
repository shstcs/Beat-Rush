using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI_Popup_Option : MonoBehaviour
{
    private void OnEnable()
    {
        Time.timeScale = 0.0f;
    }
    public void LoadStage()
    {
        SceneManager.LoadScene("Minho");
    }

    public void LoadStart()
    {
        SceneManager.LoadScene("StartUI_Test_Scene");
    }

    public void OffOption()
    {
        Time.timeScale = 1.0f;
        gameObject.SetActive(false);
    }
}

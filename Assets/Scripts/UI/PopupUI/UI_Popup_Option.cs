using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI_Popup_Option : MonoBehaviour
{
    private void OnEnable()
    {
        Time.timeScale = 0.0f;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void LoadStage()
    {
        SceneManager.LoadScene("Minho");
    }

    public void LoadStart()
    {
        SceneManager.LoadScene("Lobby");
    }

    public void OffOption()
    {
        if (SceneManager.GetActiveScene().name == "Minho" || SceneManager.GetActiveScene().name == "StageUI_Test_Scene")
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        Time.timeScale = 1.0f;
        gameObject.SetActive(false);
        SoundManager.Instance.ContinueBGM();        //음악 재생
    }
}

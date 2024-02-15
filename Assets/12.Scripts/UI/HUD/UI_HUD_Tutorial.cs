using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting.FullSerializer.Internal;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI_HUD_Tutorial : MonoBehaviour
{
    private void OnEnable()
    {
        Managers.Game.OnStageEnd += OnTutorialResult;
    }
    private void Update()
    {
        gameObject.transform.GetChild(0).transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = Managers.Game.delay.ToString("N2");
    }
    private void OnTutorialResult()
    {
        gameObject.transform.GetChild(1).gameObject.SetActive(true);
        gameObject.transform.GetChild(1).transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = Managers.Game.delay.ToString("N2");
    }
    public void OnTutorial()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        SceneManager.LoadScene("Tutorial");
    }
    public void OnLobby()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        SceneManager.LoadScene("Lobby");
    }

    private void OnDisable()
    {
        Managers.Game.OnStageEnd -= OnTutorialResult;
    }
}

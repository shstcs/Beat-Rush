using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class UI_Scene_Stage : MonoBehaviour
{
    private void Start()
    {
        Time.timeScale = 1.0f;
        Managers.UI.SetUI();
        Managers.Game.GetKeyDown += OnOption;
    }

    private void Update()
    {
        //옵션 창 여는 부분은 나중에 Input System으로 처리해도 될 것 같습니다.
        if (Input.GetKeyDown(KeyCode.Escape))
            Managers.Game.GetKeyDown?.Invoke();
    }

    private void OnOption()
    {
        GameObject.Find("Canvas").transform.GetChild(0).gameObject.SetActive(true);
    }

    private void OnDisable()
    {
        Managers.Game.GetKeyDown -= OnOption;
    }
}

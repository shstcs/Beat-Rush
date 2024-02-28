using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public interface IPopup
{
    void OffPopup();
}

public class PopupManager
{
    private IPopup _currentPopup;
    public float pauseTime = 3f;
    public IPopup CurrentPopup
    {
        set
        {
            _currentPopup = value;
            if (_currentPopup == null)
            {
                SetPopup();
            }
            else
            {
                UnsetPopup();
            }
        }
    }

    public bool IsPopupActive()
    {
        return _currentPopup != null;
    }

    public void OffPopupWindow(InputAction.CallbackContext context)
    {
        if (_currentPopup == null)
        {
            return;
        }

        _currentPopup.OffPopup();
    }

    private void SetPopup()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        Time.timeScale = 1.0f;
        Managers.Game.lockType = InputLockType.UnLock;
        if (Managers.Player != null) Managers.Player.ChangeIdleState();
    }

    private void UnsetPopup()
    {
        Managers.Game.lockType = InputLockType.Lock;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        Time.timeScale = 0.0f;
    }

    public IEnumerator DelayContinue()
    {
        Time.timeScale = 0;
        GameObject timer = GameObject.Find("Canvas").transform.GetChild(7).gameObject;
        timer.SetActive(true);
        float startRealTime = Time.realtimeSinceStartup;
        while (Time.realtimeSinceStartup - startRealTime < 3f)
        {
            yield return null;
            pauseTime = Time.realtimeSinceStartup - startRealTime;
            if( pauseTime >= 2.0f && pauseTime < 3.0f )
            {
                timer.GetComponent<TextMeshProUGUI>().text = 1.ToString();
            }
            else if(pauseTime >= 1.0f && pauseTime < 2.0f)
            {
                timer.GetComponent<TextMeshProUGUI>().text = 2.ToString();
            }
            else if (pauseTime >= 0.0f && pauseTime < 1.0f)
            {
                timer.GetComponent<TextMeshProUGUI>().text = 3.ToString();
            }
        }
        timer.SetActive(false);
        Continue();
    }

    private void Continue()
    {
        List<GameObject> notes = Managers.Pool.GetActiveNotes();
        float distance = 32.5f;
        if (notes.Count > 0)
        {
            distance = notes[0].transform.position.z - 12;
            float time = distance / (Managers.Game.stageInfos[Managers.Game.currentStage].noteSpeed * Managers.Game.speedModifier);
            Managers.Sound.ContinueBGM(time);   //음악 재생
        }
        else
        {
            Managers.Sound.ContinueBGM();
        }
        Time.timeScale = 1;
    }
}

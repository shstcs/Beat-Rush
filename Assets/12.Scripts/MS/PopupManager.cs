using System.Collections;
using System.Collections.Generic;
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
        float startRealTime = Time.realtimeSinceStartup;
        while (Time.realtimeSinceStartup - startRealTime < 3f)
        {
            pauseTime = Time.realtimeSinceStartup - startRealTime;
            yield return null;
        }
        Continue();
    }

    private void Continue()
    {
        List<GameObject> notes = Managers.Pool.GetActiveNotes();
        float distance = 32.5f;
        if (notes.Count > 0)
        {
            distance = notes[0].transform.position.z - 12;
            float time = distance / (Managers.Game.noteSpeed[Managers.Game.currentStage] * Managers.Game.speedModifier);
            Managers.Sound.ContinueBGM(time);   //음악 재생
        }
        else
        {
            Managers.Sound.ContinueBGM();
        }
        Time.timeScale = 1;
    }
}

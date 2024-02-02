using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI_HUD_Lobby : MonoBehaviour
{
    public void StartStage()
    {
        switch (Managers.Game.currentStage)
        {
            case 0:
                SceneManager.LoadScene("Tutorial");
                break;
            case 1:
                Debug.Log(Managers.Player.Data.StateData.CurrentClearStage);
                if(Managers.Player.Data.StateData.CurrentClearStage >= 1)
                {
                    SceneManager.LoadScene("Stage_1");
                }
                break;
            case 2:
                Debug.Log(Managers.Player.Data.StateData.CurrentClearStage);
                if (Managers.Player.Data.StateData.CurrentClearStage >= 2)
                {
                    SceneManager.LoadScene("Stage_2");
                }
                break;
            case 3:
                break;
        }
    }
}
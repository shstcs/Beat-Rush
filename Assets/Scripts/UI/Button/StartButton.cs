using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartButton : MonoBehaviour
{
    public void OnStart()
    {
        Managers.Sound.LoopPlayBGM(BGM.Lobby2);
        Managers.Game.GameType = GameType.Main;
        SceneManager.LoadScene("Lobby");
    }

    public void OnLoad()
    {
        if (!Managers.Data.LoadFileCheck("save"))
        {
            Debug.Log("Load File Not Exist!!!");
        }
        else if (Managers.Data.LoadFileCheck("save"))
        {
            Managers.Sound.LoopPlayBGM(BGM.Lobby2);
            Managers.Game.GameType = GameType.Main;
            SceneManager.LoadScene("Lobby");
        }
    }
}

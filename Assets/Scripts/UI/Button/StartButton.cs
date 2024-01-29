using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartButton : MonoBehaviour
{
    public void OnStart()
    {
        //Managers.Sound.LoopPlayBGM(BGM.Lobby2);
        Managers.Game.GameType = GameType.Main;
        SceneManager.LoadScene("YH-TestStage2");
    }
}

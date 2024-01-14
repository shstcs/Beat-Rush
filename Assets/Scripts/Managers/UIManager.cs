using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    private void Start()
    {
        Managers.Game.OnStageStart += SetHUD;
    }

    public void SetHUD()
    {
        //hud 府家胶 肺靛 棺 UI积己
    }
}
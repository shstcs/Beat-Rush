using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : Managers
{
    private void Awake()
    {
        Init();
    }
    private void Start()
    {
        OnStageStart += SetHUD;
    }

    public void SetHUD()
    {
        //hud 府家胶 肺靛 棺 UI积己
    }
}
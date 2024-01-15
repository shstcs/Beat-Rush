using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements.Experimental;

public class UIManager
{
    public UIManager()
    {
        Managers.Game.OnStageStart += SetStageHUD;
        Managers.Game.OnGameStart += SetStartHUD;
        Managers.Game.OnMapStart += SetMapHUD;
    }

    private void SetStageHUD()
    {
        Managers.Resource.Load<GameObject>($"UI_Stage.prefab");
    }
    private void SetStartHUD()
    {
        Managers.Resource.Load<GameObject>($"UI_Start.prefab");
    }
    private void SetMapHUD()
    {
        Managers.Resource.Load<GameObject>($"UI_Map.prefab");
    }
}
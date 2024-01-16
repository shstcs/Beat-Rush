using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements.Experimental;

public class UIManager:MonoBehaviour
{
    public void Init()
    {
        Managers.Game.OnStageStart += SetStageHUD;
        Managers.Game.OnGameStart += SetStartHUD;
        Managers.Game.OnMapStart += SetMapHUD;
    }

    private void SetStageHUD()
    {
        Instantiate(Managers.Resource.Load<GameObject>($"UI_Stage.prefab"));
    }
    private void SetStartHUD()
    {
        Instantiate(Managers.Resource.Load<GameObject>($"UI_Start.prefab"));
    }
    private void SetMapHUD()
    {
        Instantiate(Managers.Resource.Load<GameObject>($"UI_Map.prefab"));
    }
}
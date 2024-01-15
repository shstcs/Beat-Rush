using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Scene_Start : MonoBehaviour
{
    private void Start()
    {
        Managers.Game.CallGameStart();
    }
}

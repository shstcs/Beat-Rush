using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class UI_Scene_Start : MonoBehaviour
{
    private void Start()
    {
        Managers.Resource.LoadAllAsync<UnityEngine.Object>("Preloads", (key, count, totalCount) =>
        {
            if (count >= totalCount)
            {
                Managers.UI.SetUI();
            }
        });
    }
}
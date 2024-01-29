using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LobbySaveButton : MonoBehaviour
{
    private DataManager dataManager;
    private void Start()
    {
        dataManager = FindObjectOfType<DataManager>();

        if (dataManager == null)
        {
            Debug.Log("DataManager not found");
        }
    }
    public void OnButtonClick()
    {
        if (dataManager != null)
        {
            dataManager.SaveData();
        }
    }
}

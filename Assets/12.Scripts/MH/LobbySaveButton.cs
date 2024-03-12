using UnityEngine;

public class LobbySaveButton : MonoBehaviour
{
    private DataManager dataManager;
    private void Start()
    {
        ////dataManager = FindObjectOfType<DataManager>();

        //if (dataManager == null)
        //{
        //    Debug.Log("DataManager not found");
        //}
    }
    public void OnButtonClick()
    {
        Managers.Data.SaveData();
        //if (dataManager != null)
        //{
        //    dataManager.SaveData();
        //}
    }
}

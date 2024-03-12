using UnityEngine;

public class UI_Scene_Start : MonoBehaviour
{
    private void Start()
    {
        Time.timeScale = 1.0f;
        Managers.Resource.LoadAllAsync<UnityEngine.Object>("PreLoads", (key, count, totalCount) =>
        {
            if (count >= totalCount)
            {
                Managers.UI.SetUI();
                Managers.Sound.Initialized();
                Managers.Sound.LoopPlayBGM(BGM.StartBGM);
                Managers.Data.LoadSoundData();
            }
        });
    }
}
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public void SetUI()
    {
        if (SceneManager.GetActiveScene().name == "Start")
        {
            SetStartHUD();
        }
        else if (SceneManager.GetActiveScene().name == "Stage_1" || SceneManager.GetActiveScene().name == "Stage_2" || SceneManager.GetActiveScene().name == "Stage_3")
        {
            SetStageHUD();
        }
        else if (SceneManager.GetActiveScene().name == "Lobby")
        {
            SetMapHUD();
        }
        else if (SceneManager.GetActiveScene().name == "Tutorial")
        {
            SetTutorialHUD();
        }
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
        Instantiate(Managers.Resource.Load<GameObject>($"UI_Lobby.prefab"));
    }
    private void SetTutorialHUD()
    {
        Instantiate(Managers.Resource.Load<GameObject>($"UI_Tutorial.prefab"));
    }
}
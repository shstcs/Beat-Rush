using UnityEngine;

public class Smoke : MonoBehaviour
{
    void Start()
    {
        if (Managers.Game.mode != GameMode.Sudden)
            gameObject.SetActive(false);
    }
}

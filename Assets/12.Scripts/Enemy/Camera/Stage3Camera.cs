using UnityEngine;

public class Stage3Camera : MonoBehaviour
{
    [SerializeField] private NoteManager _noteManager;

    public void Stage3Start()
    {
        _noteManager.StageStart();
    }
}

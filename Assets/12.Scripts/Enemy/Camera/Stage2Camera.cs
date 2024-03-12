using UnityEngine;

public class Stage2Camera : MonoBehaviour
{
    [SerializeField] private NoteManager _noteManager;

    public void Stage2Start()
    {
        _noteManager.StageStart();
    }
}

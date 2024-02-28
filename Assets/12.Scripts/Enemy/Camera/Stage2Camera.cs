using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage2Camera : MonoBehaviour
{
    [SerializeField] private NoteManager _noteManager;

    public void Stage2Start()
    {
        _noteManager.StageStart();
    }
}

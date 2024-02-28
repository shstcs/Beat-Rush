using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage1Camera : MonoBehaviour
{
    [SerializeField] private NoteManager _noteManager;

    public void Stage1Start()
    {
        _noteManager.StageStart();
    }
}

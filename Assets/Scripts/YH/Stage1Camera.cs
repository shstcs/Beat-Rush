using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage1Camera : MonoBehaviour
{
    [SerializeField] private GameObject _camera;
    public void FixLotation()
    {
        _camera.transform.position = gameObject.transform.position;
    }
}

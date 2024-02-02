using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VirtualCameraController : MonoBehaviour
{

    private CinemachineVirtualCamera _cam;

    private float _verticalAxis;
    private float _horizontalAxis;
    private void Start()
    {
        _cam = GetComponent<CinemachineVirtualCamera>();
    }

    private void Update()
    {
        if(Time.timeScale == 0f)
        {
            _cam.GetCinemachineComponent<CinemachinePOV>().m_VerticalAxis.Value = _verticalAxis;
            _cam.GetCinemachineComponent<CinemachinePOV>().m_HorizontalAxis.Value = _horizontalAxis;
        }
        else
        {
            _verticalAxis = _cam.GetCinemachineComponent<CinemachinePOV>().m_VerticalAxis.Value;
            _horizontalAxis = _cam.GetCinemachineComponent<CinemachinePOV>().m_HorizontalAxis.Value;
        }
    }

}

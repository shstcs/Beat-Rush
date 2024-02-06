using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VirtualCameraController : MonoBehaviour
{

    private CinemachinePOV _camPOV;

    private float _verticalAxis;
    private float _horizontalAxis;
    private void Start()
    {
        _camPOV = GetComponent<CinemachineVirtualCamera>().GetCinemachineComponent<CinemachinePOV>();
        _camPOV.m_VerticalAxis.Value = Managers.Game.CinemachinemVerticalAxisValue;
        _camPOV.m_HorizontalAxis.Value = Managers.Game.CinemachinemmHorizontalAxisValue;
    }

    private void Update()
    {
        if(Time.timeScale == 0f)
        {
            _camPOV.m_VerticalAxis.Value = _verticalAxis;
            _camPOV.m_HorizontalAxis.Value = _horizontalAxis;

            Managers.Game.CinemachinemVerticalAxisValue = _camPOV.m_VerticalAxis.Value;
            Managers.Game.CinemachinemmHorizontalAxisValue = _camPOV.m_HorizontalAxis.Value;
        }
        else
        {
            _verticalAxis = _camPOV.m_VerticalAxis.Value;
            _horizontalAxis = _camPOV.m_HorizontalAxis.Value;
        }
    }

}

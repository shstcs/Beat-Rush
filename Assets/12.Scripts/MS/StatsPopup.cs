using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class StatsPopup : MonoBehaviour
{
    [SerializeField] private GameObject _statsPopup;
    public PlayerInput Input { get; private set; }

    private void Awake()
    {
        Input = GetComponent<PlayerInput>();
    }

    private void Start()
    {
        Input.PlayerActions.StatPopup.started += OpenStatsPopup;
    }

    private void OpenStatsPopup(InputAction.CallbackContext context)
    {
        if(_statsPopup.activeSelf)
            _statsPopup.SetActive(false);
        else
            _statsPopup.SetActive(true);
    }

    private void OnDisable()
    {
        Input.PlayerActions.StatPopup.started -= OpenStatsPopup;
    }
}

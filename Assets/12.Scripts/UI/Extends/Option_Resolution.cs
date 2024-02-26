using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Option_Resolution : MonoBehaviour
{
    FullScreenMode screenMode;
    private bool _isFull;

    private void Awake()
    {
        _isFull = Screen.fullScreen;
        screenMode = Screen.fullScreenMode;
    }
    public void qHD()
    {
        Screen.SetResolution(960, 540, _isFull);
    }
    public void HD()
    {
        Screen.SetResolution(1280, 720, _isFull);
    }
    public void FHD()
    {
        Screen.SetResolution(1920, 1080, _isFull);
    }
}

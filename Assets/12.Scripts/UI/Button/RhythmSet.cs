using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RhythmSet : MonoBehaviour
{
    private GameObject RhythmSpeed;
    private void Start()
    {
        RhythmSpeed = transform.GetChild(0).gameObject;
        SetRhythm();
    }

    public void IncreaseRhythmSpeed1()
    {
        Managers.Game.delay += 0.01f;
        SetRhythm();
    }
    public void IncreaseRhythmSpeed2()
    {
        Managers.Game.delay += 0.05f;
        SetRhythm();
    }
    public void DecreaseRhythmSpeed1()
    {
        Managers.Game.delay -= 0.01f;
        SetRhythm();
    }
    public void DecreaseRhythmSpeed2()
    {
        Managers.Game.delay -= 0.05f;
        SetRhythm();
    }
    private void SetRhythm()
    {
        RhythmSpeed.GetComponent<TextMeshProUGUI>().text = Managers.Game.delay.ToString("N2");
    }
}

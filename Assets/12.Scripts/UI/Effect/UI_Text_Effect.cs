using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UI_Text_Effect : MonoBehaviour
{
    private void Start()
    {
        Managers.Game.OnCheckNote += OnBounceEffect;
    }
    private void OnBounceEffect()
    {
        StartCoroutine(BounceEffect());
    }
    private IEnumerator BounceEffect()
    {
        float size = 0.3f;
        float upTimeSize = size;
        while (upTimeSize > 0.0f)
        {
            upTimeSize -= (size/0.3f) * Time.deltaTime;
            transform.localScale = Vector3.one * (1+upTimeSize);
            yield return null;
        }
        transform.localScale = Vector3.one;
    }
    private void OnDisable()
    {
        StopCoroutine(BounceEffect());
        Managers.Game.OnCheckNote -= OnBounceEffect;
    }
}

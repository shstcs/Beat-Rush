using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Blink_Effect : MonoBehaviour
{
    private float _time;

    private void Update()
    {
        if(_time < 0.5f)
        {
            GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1 - _time);
        }
        else
        {
            GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, _time);
            if (_time > 1f)
                _time = 0;
        }
        _time += 0.01f;
    }
}

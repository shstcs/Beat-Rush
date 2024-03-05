using System.Collections;
using TMPro;
using UnityEngine;

public class UI_Blink_Effect : MonoBehaviour
{
    private float _time;
    private void OnEnable()
    {
        BlinkUI();
    }
    private void BlinkUI()
    {
        StartCoroutine(OnBlinkUI());
    }
    private IEnumerator OnBlinkUI()
    {
        _time = Time.realtimeSinceStartup;
        float _blinktime;
        while (Time.realtimeSinceStartup - _time < 2f)
        {
            _blinktime = Time.realtimeSinceStartup - _time;
            if (_blinktime < 0.5f)
            {
                GetComponent<TextMeshProUGUI>().color = new Color(0, 0, 0, 1 - _blinktime);
            }
            else if (_blinktime >= 1f)
            {
                _time = Time.realtimeSinceStartup;
            }
            else
            {
                GetComponent<TextMeshProUGUI>().color = new Color(0, 0, 0, _blinktime);
            }
            yield return null;
        }
    }
}

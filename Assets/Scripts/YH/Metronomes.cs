using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Metronomes : MonoBehaviour
{
    private float rotationSpeed = 40f; // 회전 속도 (40도/초)
    private float oscillationDuration = 0.6666f; // 왕복 시간
    private float _timer;

    private bool isMovingRight = true;
    void Update()
    {
        //-40~40으로 음악에 맞게 움직이도록 조정
        if (Managers.Sound.PlayTime() > 0)
        {
            _timer += Time.deltaTime;
            // 회전 각도 계산
            float rotationAngle = rotationSpeed * Time.deltaTime;

            // 왕복 로직
            if (isMovingRight)
            {
                transform.Rotate(Vector3.up, rotationAngle);

                // 오른쪽으로 이동 중일 때 타이머 체크
                if (_timer >= oscillationDuration)
                {
                    _timer = 0;
                    isMovingRight = false; // 왼쪽으로 이동으로 변경
                }
            }
            else
            {
                transform.Rotate(Vector3.up, -rotationAngle);

                // 왼쪽으로 이동 중일 때 타이머 체크
                if (_timer >= oscillationDuration)
                {
                    _timer = 0;
                    isMovingRight = true; // 오른쪽으로 이동으로 변경
                }
            }
        }
    }
}

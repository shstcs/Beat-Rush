using UnityEngine;

public class Metronomes : MonoBehaviour
{
    private float rotationSpeed = 40f; // 회전 속도 (40도/초)
    private float oscillationDuration = 0.666666f; // 왕복 시간
    private double _curDsp;
    private double _startDsp;
    private bool _isPlaying;

    private bool isMovingRight = true;
    void Update()
    {
        //-40~40으로 음악에 맞게 움직이도록 조정
        if (Managers.Sound.PlayTime() > 0)
        {
            if (!_isPlaying)
            {
                _isPlaying = true;
                _startDsp = AudioSettings.dspTime;
                _curDsp = AudioSettings.dspTime;
            }

            // 회전 각도 계산
            float rotationAngle = rotationSpeed * (float)(AudioSettings.dspTime - _curDsp);
            _curDsp = AudioSettings.dspTime;

            // 왕복 로직
            if (isMovingRight)
            {
                transform.Rotate(Vector3.up, rotationAngle);

                // 오른쪽으로 이동 중일 때 타이머 체크
                if ((float)(AudioSettings.dspTime - _startDsp) >= oscillationDuration)
                {
                    _startDsp = AudioSettings.dspTime;
                    isMovingRight = false; // 왼쪽으로 이동으로 변경
                }
            }
            else
            {
                transform.Rotate(Vector3.up, -rotationAngle);

                // 왼쪽으로 이동 중일 때 타이머 체크
                if ((float)(AudioSettings.dspTime - _startDsp) >= oscillationDuration)
                {
                    _startDsp = AudioSettings.dspTime;
                    isMovingRight = true; // 오른쪽으로 이동으로 변경
                }
            }
        }
    }
}

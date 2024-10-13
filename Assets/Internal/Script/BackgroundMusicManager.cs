using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMusicManager : MonoBehaviour
{
    AudioManager audioManager;
    private bool isPaused = true;

    private void Start()
    {
        audioManager = AudioManager.Instance;
        audioManager.AudioPause(false);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // 시간 스케일과 오디오 피치를 일시정지 상태에 따라 설정
            audioManager.AudioPause(isPaused); // 배경음악 멈춤 또는 재개
            isPaused = !isPaused;
        }
    }
}

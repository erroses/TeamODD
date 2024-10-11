using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenOption : MonoBehaviour
{
    // 게임이 멈췄는지 확인하는 변수
    private bool isPaused = false;

    void Update()
    {
        // ESC 키를 눌렀을 때
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                // 게임을 재개
                Time.timeScale = 1f;
                isPaused = false;
            }
            else
            {
                // 게임을 멈춤
                Time.timeScale = 0f;
                isPaused = true;
            }
        }
    }
}

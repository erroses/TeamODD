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
            Time.timeScale = isPaused ? 1f : 0f; // 게임을 재개하거나 멈춤
            isPaused = !isPaused; // 일시 정지 상태를 토글
        }
    }
}

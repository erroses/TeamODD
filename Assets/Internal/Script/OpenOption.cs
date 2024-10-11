using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenOption : MonoBehaviour
{
    // ������ ������� Ȯ���ϴ� ����
    private bool isPaused = false;

    void Update()
    {
        // ESC Ű�� ������ ��
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                // ������ �簳
                Time.timeScale = 1f;
                isPaused = false;
            }
            else
            {
                // ������ ����
                Time.timeScale = 0f;
                isPaused = true;
            }
        }
    }
}

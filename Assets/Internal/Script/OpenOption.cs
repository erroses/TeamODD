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
            Time.timeScale = isPaused ? 1f : 0f; // ������ �簳�ϰų� ����
            isPaused = !isPaused; // �Ͻ� ���� ���¸� ���
        }
    }
}

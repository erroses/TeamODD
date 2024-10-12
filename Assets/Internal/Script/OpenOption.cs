using UnityEngine;

public class OpenOption : MonoBehaviour
{
    // ������ ������� Ȯ���ϴ� ����
    public bool isPaused;
    public GameObject Panel;

    private void Start()
    {
        isPaused = false;
        Time.timeScale = 1f;
    }

    void Update()
    {
        // ESC Ű�� ������ ��
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Panel.SetActive(!isPaused);
            Time.timeScale = isPaused ? 1f : 0f; // ������ �簳�ϰų� ����
            isPaused = !isPaused; // �Ͻ� ���� ���¸� ���
        }
    }
}

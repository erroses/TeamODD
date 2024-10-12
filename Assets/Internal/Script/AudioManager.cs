using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private AudioSource audioSource;
    public AudioClip clip;

    private bool isPaused = false;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();

        audioSource.clip = this.clip;
        audioSource.loop = true; // �ݺ� ��� ����
        audioSource.Play(); // ���������� ���
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // �ð� �����ϰ� ����� ��ġ�� �Ͻ����� ���¿� ���� �����մϴ�.
            Time.timeScale = isPaused ? 0f : 1f;
            audioSource.pitch = isPaused ? 0f : 1f; // ������� ���� �Ǵ� �簳

            // ������ �Ͻ����� ���¸� ��ȯ�մϴ�.
            isPaused = !isPaused;
        }
    }
}

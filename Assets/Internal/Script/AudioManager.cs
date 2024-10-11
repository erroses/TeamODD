using UnityEngine;

public class AudioManager : MonoBehaviour
{
    AudioSource audioSource;
    public AudioClip clip;

    private bool isPaused = false;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        audioSource.clip = this.clip;
        audioSource.Play(); // �ݺ� ���, ���������� ���
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                // ���� �簳
                Time.timeScale = 1f;
                audioSource.pitch = 1f; // ������� �簳
                isPaused = false;
            }
            else
            {
                // ���� �Ͻ�����
                Time.timeScale = 0f;
                audioSource.pitch = 0f; // ������� ����
                isPaused = true;
            }
        }
    }
}

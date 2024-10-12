using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private static AudioManager instance;
    private AudioSource audioSource;
    public AudioClip clip;

    private bool isPaused = false;

    private void Awake()
    {
        // Singleton pattern
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // �� ������Ʈ�� �� ��ȯ �� �ı����� �ʵ��� ����
        }
        else
        {
            Destroy(gameObject); // �̹� �����ϴ� �ν��Ͻ��� ���� ��� ���� ������Ʈ�� �ı�
        }
    }

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

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
            DontDestroyOnLoad(gameObject); // 이 오브젝트를 씬 전환 시 파괴되지 않도록 설정
        }
        else
        {
            Destroy(gameObject); // 이미 존재하는 인스턴스가 있을 경우 현재 오브젝트를 파괴
        }
    }

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();

        audioSource.clip = this.clip;
        audioSource.loop = true; // 반복 재생 설정
        audioSource.Play(); // 지속적으로 재생
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // 시간 스케일과 오디오 피치를 일시정지 상태에 따라 설정합니다.
            Time.timeScale = isPaused ? 0f : 1f;
            audioSource.pitch = isPaused ? 0f : 1f; // 배경음악 멈춤 또는 재개

            // 게임의 일시정지 상태를 전환합니다.
            isPaused = !isPaused;
        }
    }
}

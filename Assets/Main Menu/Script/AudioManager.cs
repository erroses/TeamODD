using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private static AudioManager instance;
    private AudioSource audioSource;
    public AudioClip clip;
    public static AudioManager Instance
    {
        get
        {
            if (instance == null)
            {
                Debug.LogError("AudioManager instance is null. Make sure it is in the scene.");
            }
            return instance;
        }
    }
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

    public void AudioPause(bool isPaused)
    {
        audioSource.pitch = isPaused ? 0f : 1f;
    }
}

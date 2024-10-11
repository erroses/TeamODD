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
        audioSource.Play(); // 반복 재생, 지속적으로 재생
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                // 게임 재개
                Time.timeScale = 1f;
                audioSource.pitch = 1f; // 배경음악 재개
                isPaused = false;
            }
            else
            {
                // 게임 일시정지
                Time.timeScale = 0f;
                audioSource.pitch = 0f; // 배경음악 멈춤
                isPaused = true;
            }
        }
    }
}

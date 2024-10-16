using UnityEngine;

public class PlayerSound : MonoBehaviour
{
    public AudioClip playerAttack; // 오디오 클립: 플레이어 타격
    private AudioSource audioSource; // AudioSource 컴포넌트\

    void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;
        audioSource.loop = true;
    }

    public void audioSound()
    {
        audioSource.PlayOneShot(playerAttack);
    }
}

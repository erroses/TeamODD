using UnityEngine;

public class PlayerSound : MonoBehaviour
{
    public AudioClip playerAttack; // ����� Ŭ��: �÷��̾� Ÿ��
    private AudioSource audioSource; // AudioSource ������Ʈ\

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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSound : MonoBehaviour
{
    public AudioClip playerAttack; // ����� Ŭ��: �÷��̾� Ÿ��
    private AudioSource audioSource; // AudioSource ������Ʈ
    // Start is called before the first frame update
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

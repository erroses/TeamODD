using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSound : MonoBehaviour
{
    public AudioClip playerAttack; // ����� ����� Ŭ��: �׾Ƹ� �ļ�
    private AudioSource audioSource; // AudioSource ������Ʈ
    // Start is called before the first frame update
    void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;
        audioSource.clip = playerAttack;
    }

    public void audioSound()
    {
        audioSource.Play();
    }
}

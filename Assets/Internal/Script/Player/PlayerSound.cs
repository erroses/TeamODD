using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSound : MonoBehaviour
{
    public AudioClip playerAttack; // 재생할 오디오 클립: 항아리 파손
    private AudioSource audioSource; // AudioSource 컴포넌트
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

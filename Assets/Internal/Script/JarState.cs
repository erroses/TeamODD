using UnityEngine;

public class JarState : MonoBehaviour
{
    public Material[] material = new Material[4]; // 이미지로 대체

    private AudioSource audioSource; // AudioSource 컴포넌트
    public AudioClip jarRepair; // 재생할 오디오 클립: 항아리 수리
    public AudioClip jarDamage; // 재생할 오디오 클립: 항아리 손상
    public AudioClip jarCrash; // 재생할 오디오 클립: 항아리 파손

    public int maxHealth = 3;
    public int currentHealth;
    private void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;

        currentHealth = maxHealth;
    }

    /// <summary>
    /// 항아리 상태에 따라 색 변경 및 소리 재생
    /// </summary>
    /// <param name="isRepair"></param>
    public void UpdateJarState(bool isRepair)
    {
        // Renderer 컴포넌트를 가져옵니다.
        Renderer renderer = GetComponent<Renderer>();
        renderer.material = material[currentHealth];

        switch (currentHealth)
        {
            case 0: audioSource.clip = jarCrash; break;
            case 1:
            case 2: if (!isRepair) { audioSource.clip = jarDamage; } break; // 부분파손 또는 수리 효과음 구분
        }

        if(isRepair) { audioSource.clip = jarRepair; }

        audioSource.Play();
    }
}

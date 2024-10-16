using System.Collections;
using UnityEngine;

public class JarState : MonoBehaviour
{
    public Material[] material = new Material[4]; // �̹����� ��ü
    [SerializeField]
    private GameObject[] models = new GameObject[4];

    private AudioSource audioSource; // AudioSource ������Ʈ
    public AudioClip jarRepair; // ����� ����� Ŭ��: �׾Ƹ� ����
    public AudioClip jarDamage; // ����� ����� Ŭ��: �׾Ƹ� �ջ�
    public AudioClip jarCrash; // ����� ����� Ŭ��: �׾Ƹ� �ļ�
    public Animator animator;

    public int maxHealth = 3;
    public int currentHealth;

    public int health;
    public bool pendingDestroy = false;
    public JarObjectData jarObjectData;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        SetHealthPoint(maxHealth, true);
    }

    private void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;
    }

    public void SetHealthPoint(int healthPoint, bool init = false)
    {
        if (pendingDestroy)
        {
            return;
        }
        currentHealth = healthPoint;
        if (!init)
        {
            jarObjectData.HealthPoint = currentHealth;
        }
        UpdateModel(healthPoint);
    }

    public void UpdateModel(int healthPoint)
    {
        for (int i = 0; i < models.Length; i++)
        {
            models[i].SetActive(i == healthPoint);
        }
    }

    /// <summary>
    /// �׾Ƹ� ���¿� ���� �� ���� �� �Ҹ� ���
    /// </summary>
    /// <param name="isRepair"></param>
    public void UpdateJarState(bool isRepair)
    {
        // Renderer ������Ʈ�� �����ɴϴ�.
        // Renderer renderer = GetComponent<Renderer>();
        // renderer.material = material[currentHealth];

        switch (currentHealth)
        {
            case 0: audioSource.clip = jarCrash; break;
            case 1:
            case 2: if (!isRepair) { audioSource.clip = jarDamage; } break; // �κ��ļ� �Ǵ� ���� ȿ���� ����
        }

        if (isRepair) { audioSource.clip = jarRepair; }

        audioSource.Play();
        animator.SetTrigger(isRepair ? "HealTrigger" : "AttackTrigger");
    }

    private void FadeOut()
    {
        StartCoroutine(FadeOutAnimationCoroutine());
    }

    private IEnumerator FadeOutAnimationCoroutine()
    {
        yield return new WaitForSeconds(1.2f);
        Destroy(gameObject);
    }
}

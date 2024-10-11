using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    public AudioClip jarDamage; // 재생할 오디오 클립: 플레이어 타격
    private Rigidbody rb;
    public float power = 5f;
    void Start()
    {
        rb = GetComponent<Rigidbody>();

    }
    void OnCollisionStay(Collision other)
    {
        // 플레이어 간 충돌 구현
        if (other.gameObject.CompareTag("player"))
        {
            Vector3 KnockBackVelocity = Vector3.zero;

            if (other.gameObject.transform.position.x > transform.position.x)
            {
                if (other.gameObject.transform.position.z > transform.position.z)
                {
                    KnockBackVelocity = new Vector3(-power, 0f, -power); // 값 증가
                }
                else
                {
                    KnockBackVelocity = new Vector3(-power, 0f, power);
                }
            }
            else
            {
                if (other.gameObject.transform.position.z > transform.position.z)
                {
                    KnockBackVelocity = new Vector3(power, 0f, -power); // 값 증가
                }
                else
                {
                    KnockBackVelocity = new Vector3(power, 0f, power);
                }
            }

            // 넉백 효과 적용
            rb.AddForce(KnockBackVelocity, ForceMode.Impulse);
        }
    }
}
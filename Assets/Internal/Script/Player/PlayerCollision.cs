using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    private Rigidbody rb;
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
                    KnockBackVelocity = new Vector3(-5f, 2f, -5f); // 값 증가
                }
                else
                {
                    KnockBackVelocity = new Vector3(-5f, 2f, 5f);
                }
            }
            else
            {
                if (other.gameObject.transform.position.z > transform.position.z)
                {
                    KnockBackVelocity = new Vector3(5f, 2f, -5f); // 값 증가
                }
                else
                {
                    KnockBackVelocity = new Vector3(5f, 2f, 5f);
                }
            }

            // 넉백 효과 적용
            rb.AddForce(KnockBackVelocity, ForceMode.Impulse);
        }
    }
}
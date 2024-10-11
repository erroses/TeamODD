using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
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

            // 상대적 위치에 따른 방향 계산
            float directionX = (other.gameObject.transform.position.x > transform.position.x) ? -1f : 1f;
            float directionZ = (other.gameObject.transform.position.z > transform.position.z) ? -1f : 1f;

            // 넉백 벡터 계산
            KnockBackVelocity = new Vector3(directionX * power, 0f, directionZ * power);

            // 넉백 효과 적용
            rb.AddForce(KnockBackVelocity, ForceMode.Impulse);

        }
    }
}
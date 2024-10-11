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
        // �÷��̾� �� �浹 ����
        if (other.gameObject.CompareTag("player"))
        {
            Vector3 KnockBackVelocity = Vector3.zero;

            // ����� ��ġ�� ���� ���� ���
            float directionX = (other.gameObject.transform.position.x > transform.position.x) ? -1f : 1f;
            float directionZ = (other.gameObject.transform.position.z > transform.position.z) ? -1f : 1f;

            // �˹� ���� ���
            KnockBackVelocity = new Vector3(directionX * power, 0f, directionZ * power);

            // �˹� ȿ�� ����
            rb.AddForce(KnockBackVelocity, ForceMode.Impulse);

        }
    }
}
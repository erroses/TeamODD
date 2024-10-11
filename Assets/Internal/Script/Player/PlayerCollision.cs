using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    public AudioClip jarDamage; // ����� ����� Ŭ��: �÷��̾� Ÿ��
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

            if (other.gameObject.transform.position.x > transform.position.x)
            {
                if (other.gameObject.transform.position.z > transform.position.z)
                {
                    KnockBackVelocity = new Vector3(-power, 0f, -power); // �� ����
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
                    KnockBackVelocity = new Vector3(power, 0f, -power); // �� ����
                }
                else
                {
                    KnockBackVelocity = new Vector3(power, 0f, power);
                }
            }

            // �˹� ȿ�� ����
            rb.AddForce(KnockBackVelocity, ForceMode.Impulse);
        }
    }
}
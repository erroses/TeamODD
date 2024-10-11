using UnityEngine;

public class AttackCollision : MonoBehaviour
{
    private Rigidbody rb;
    Transform parent;
    GameObject parentObject;

    private void Start()
    {
        rb = this.GetComponent<Rigidbody>();
        parent = transform.parent;
        parentObject = parent.gameObject;
    }

    void OnTriggerStay(Collider other)
    {
        // �÷��̾�� �ε����� ���
        if (other.gameObject.CompareTag("player") && other.gameObject != parentObject)
        {
            Vector3 KnockBackVelocity = Vector3.zero;

            if (parent.position.x > other.transform.position.x)
            {
                if (parent.position.z > other.transform.position.z)
                {
                    KnockBackVelocity = new Vector3(-7f, 2f, -7f); // �� ����
                }
                else
                {
                    KnockBackVelocity = new Vector3(-7f, 2f, 7f);
                }
            }
            else
            {
                if (parent.position.z > other.transform.position.z)
                {
                    KnockBackVelocity = new Vector3(7f, 2f, -7f); // �� ����
                }
                else
                {
                    KnockBackVelocity = new Vector3(7f, 2f, 7f);
                }
            }

            Rigidbody otherRb = other.gameObject.GetComponent<Rigidbody>();

            // ��뿡�� �˹� ȿ�� ����
            otherRb.AddForce(KnockBackVelocity, ForceMode.Impulse);
        }

        // �׾Ƹ��� �ε����� ���
    }
}
using Unity.VisualScripting;
using UnityEngine;

public class MovePlayer2 : MonoBehaviour
{
    private Rigidbody rb;
    PlayerState PlayerState;
    Transform child;

    private void Start()
    {
        rb = this.GetComponent<Rigidbody>();
        PlayerState = this.GetComponent<PlayerState>();
        child = transform.GetChild(0);
    }

    /// <summary>
    /// �÷��̾��� �⺻���� Ű �Է� ó��
    /// </summary>
    void Update()
    {
        float moveHorizontal = 0;
        float moveVertical = 0;

        // Ű �Է� ó��
        if (Input.GetKey(KeyCode.A)) { moveHorizontal = -1; }
        if (Input.GetKey(KeyCode.D)) { moveHorizontal = 1; }
        if (Input.GetKey(KeyCode.W)) { moveVertical = 1; }
        if (Input.GetKey(KeyCode.S)) { moveVertical = -1; }

        // ���� ���� ���
        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical).normalized;

        if (movement != Vector3.zero)
        {
            // �ڽ� ������Ʈ�� Transform�� ��������
            Transform childTransform = transform.GetChild(0); // ù ��° �ڽ� ������Ʈ

            // �̵� ������ �������� �θ� ���� ���� ȸ��
            Quaternion targetRotation = Quaternion.LookRotation(movement); // �̵� �������� ȸ��

            // �θ��� Y���� �������� ȸ�� ����
            childTransform.rotation = Quaternion.Slerp(childTransform.rotation, targetRotation, Time.deltaTime * 10f); // �ε巯�� ȸ��
        }

        //// �̵� �������� ȸ��
        //if (movement != Vector3.zero)
        //{
        //    // �̵� �������� ȸ���ϰ� Y�� �������� 90�� ȸ��
        //    Quaternion targetRotation = Quaternion.LookRotation(movement) * Quaternion.Euler(0, 270, 0);
        //    child.transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 10f); // �ε巯�� ȸ��
        //}

        // ���� ����
        rb.AddForce(movement * PlayerState.power * Time.deltaTime, ForceMode.Force);

        // �ִ� �ӵ� ����
        float slowdownRate = 0.97f; // ���� ����

        if (rb.velocity.magnitude > PlayerState.maxSpeed)
        {
            rb.velocity *= slowdownRate;
        }
    }
}
using Unity.VisualScripting;
using UnityEngine;

public class MovePlayer1 : MonoBehaviour
{
    private Rigidbody rb;
    PlayerState PlayerState;
    Transform child;
    GameObject childObject;

    private void Start()
    {
        rb = this.GetComponent<Rigidbody>();
        PlayerState = this.GetComponent<PlayerState>();
        child = transform.GetChild(0);
        childObject = child.gameObject;
    }

    /// <summary>
    /// �÷��̾��� �⺻���� Ű �Է� ó��
    /// </summary>
    void Update()
    {
        float moveHorizontal = 0;
        float moveVertical = 0;

        if (Input.GetKey(KeyCode.LeftArrow)) { moveHorizontal = -1; }
        if (Input.GetKey(KeyCode.RightArrow)) { moveHorizontal = 1; }
        if (Input.GetKey(KeyCode.UpArrow)) { moveVertical = 1; }
        if (Input.GetKey(KeyCode.DownArrow)) { moveVertical = -1; }

        // ���� ���� ���
        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical).normalized;

        if (movement != Vector3.zero)
        {
            // �ڽ� ������Ʈ�� Transform�� ��������
            Transform childTransform = transform.GetChild(0); // ù ��° �ڽ� ������Ʈ

            // �̵� ������ �������� �θ� ���� ���� ȸ��
            Quaternion targetRotation = Quaternion.LookRotation(movement) * Quaternion.Euler(0, 90, 0); // �̵� �������� ȸ��

            // �θ��� Y���� �������� ȸ�� ����
            childTransform.rotation = Quaternion.Slerp(childTransform.rotation, targetRotation, Time.deltaTime * 10f); // �ε巯�� ȸ��
        }

        // �̵� �������� ȸ��
        //if (movement != Vector3.zero)
        //{
        //    // �̵� �������� ȸ���ϰ� Y�� �������� 90�� ȸ��
        //    Quaternion targetRotation = Quaternion.LookRotation(movement) * Quaternion.Euler(0, 90, 0);
        //    transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 10f); // �ε巯�� ȸ��
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
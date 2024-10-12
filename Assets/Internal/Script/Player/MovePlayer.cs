using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MovePlayer : MonoBehaviour
{
    private Rigidbody rb;
    private Transform child;

    private Dictionary<string, KeyCode[]> keyMappings;
    private Dictionary<string, float> rotationMappings;

    public float power = 10000f;
    public float maxSpeed = 25.0f;
    public float moveRegion = 100f;

    private void Start()
    {
        rb = this.GetComponent<Rigidbody>();
        child = transform.GetChild(0);

        // Ű ����
        keyMappings = new Dictionary<string, KeyCode[]>()
        {
            { "Player1", new KeyCode[] { KeyCode.LeftArrow, KeyCode.RightArrow, KeyCode.UpArrow, KeyCode.DownArrow } },
            { "Player2", new KeyCode[] { KeyCode.A, KeyCode.D, KeyCode.W, KeyCode.S } }
        };

        // ���� ����
        rotationMappings = new Dictionary<string, float>()
        {
            { "Player1", 90f },
            { "Player2", 270f }
        };
    }
    /// <summary>
    /// �÷��̾� �̵� ó��
    /// </summary>
    private void Update()
    {
        float moveHorizontal = 0;
        float moveVertical = 0;

        KeyCode[] controls = keyMappings[transform.name];

        // Ű �Է� ó��
        if (Input.GetKey(controls[0])) { moveHorizontal = -1; }
        if (Input.GetKey(controls[1])) { moveHorizontal = 1; }
        if (Input.GetKey(controls[2])) { moveVertical = 1; }
        if (Input.GetKey(controls[3])) { moveVertical = -1; }

        // ���� ���� ���
        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical).normalized;

        if (movement != Vector3.zero)
        {
            // �̵� ������ �������� �θ� ���� ���� ȸ��
            float rotationAngle = rotationMappings[transform.name];
            Quaternion targetRotation = Quaternion.LookRotation(movement) * Quaternion.Euler(0, rotationAngle, 0); // �̵� �������� ȸ��

            // �θ��� Y���� �������� ȸ�� ����
            child.rotation = Quaternion.Slerp(child.rotation, targetRotation, Time.deltaTime * 10f); // �ε巯�� ȸ��
        }

        // ���� ����
        rb.AddForce(power * Time.deltaTime * movement, ForceMode.Force);

        if (rb.position.sqrMagnitude >= moveRegion * moveRegion)
        {
            rb.position = rb.position.normalized * moveRegion;
        }

        // �ִ� �ӵ� ����
        float slowdownRate = 0.97f; // ���� ����
        if (rb.velocity.magnitude > maxSpeed) { rb.velocity *= slowdownRate; }
    }
}

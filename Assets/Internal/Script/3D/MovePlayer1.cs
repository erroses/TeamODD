using Unity.VisualScripting;
using UnityEngine;

public class MovePlayer1 : MonoBehaviour
{
    private Rigidbody rb;

    public float power = 1000f;
    public float maxSpeed = 5.0f;

    private void Start()
    {
        rb = this.GetComponent<Rigidbody>();
    }

    /// <summary>
    /// �÷��̾��� �⺻���� Ű �Է� ó��
    /// </summary>
    void Update()
    {
        float moveHorizontal = 0;
        float moveVertical = 0;

        // Ű �Է� ó��
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            moveHorizontal = -1;
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            moveHorizontal = 1;
        }
        if (Input.GetKey(KeyCode.UpArrow))
        {
            moveVertical = 1;
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            moveVertical = -1;
        }

        // ���� ���� ���
        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical).normalized;

        // ���� ����
        rb.AddForce(movement * power * Time.deltaTime);

        // �ִ� �ӵ� ����
        if (rb.velocity.magnitude > maxSpeed)
        {
            rb.velocity = rb.velocity.normalized * maxSpeed;
        }
    }
}
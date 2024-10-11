using Unity.VisualScripting;
using UnityEngine;

public class MovePlayer2 : MonoBehaviour
{
    private Rigidbody rb;

    public float power = 10000f;
    public float maxSpeed = 8.0f;

    private void Start()
    {
        rb = this.GetComponent<Rigidbody>();
    }

    /// <summary>
    /// 플레이어의 기본적인 키 입력 처리
    /// </summary>
    void Update()
    {
        float moveHorizontal = 0;
        float moveVertical = 0;

        // 키 입력 처리
        if (Input.GetKey(KeyCode.A))
        {
            moveHorizontal = -1;
        }
        if (Input.GetKey(KeyCode.D))
        {
            moveHorizontal = 1;
        }
        if (Input.GetKey(KeyCode.W))
        {
            moveVertical = 1;
        }
        if (Input.GetKey(KeyCode.S))
        {
            moveVertical = -1;
        }

        // 방향 벡터 계산
        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical).normalized;

        // 힘을 적용
        rb.AddForce(movement * power * Time.deltaTime);

        // 최대 속도 제한
        if (rb.velocity.magnitude > maxSpeed)
        {
            rb.velocity = rb.velocity.normalized * maxSpeed;
        }
    }
}
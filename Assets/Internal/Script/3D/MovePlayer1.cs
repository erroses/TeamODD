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
    /// 플레이어의 기본적인 키 입력 처리
    /// </summary>
    void Update()
    {
        float moveHorizontal = 0;
        float moveVertical = 0;

        // 키 입력 처리
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
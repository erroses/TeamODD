using Unity.VisualScripting;
using UnityEngine;

public class MovePlayer2 : MonoBehaviour
{
    private Rigidbody rb;
    PlayerState PlayerState;

    private void Start()
    {
        rb = this.GetComponent<Rigidbody>();
        PlayerState = this.GetComponent<PlayerState>();
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

        // 이동 방향으로 회전
        if (movement != Vector3.zero)
        {
            // 이동 방향으로 회전하고 Y축 기준으로 90도 회전
            Quaternion targetRotation = Quaternion.LookRotation(movement) * Quaternion.Euler(0, 270, 0);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 10f); // 부드러운 회전
        }

        // 힘을 적용
        rb.AddForce(movement * PlayerState.power * Time.deltaTime, ForceMode.Force);

        // 최대 속도 제한
        float slowdownRate = 0.97f; // 감속 비율

        if (rb.velocity.magnitude > PlayerState.maxSpeed)
        {
            rb.velocity *= slowdownRate;
        }
    }
}
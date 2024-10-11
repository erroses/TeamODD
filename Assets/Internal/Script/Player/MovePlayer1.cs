using Unity.VisualScripting;
using UnityEngine;

public class MovePlayer1 : MonoBehaviour
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
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            moveHorizontal = -1; // 왼쪽 이동
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            moveHorizontal = 1; // 오른쪽 이동
        }
        if (Input.GetKey(KeyCode.UpArrow))
        {
            moveVertical = 1; // 위쪽 이동
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            moveVertical = -1; // 아래쪽 이동
        }

        // 방향 벡터 계산
        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical).normalized;

        // 이동 방향으로 회전
        if (movement != Vector3.zero)
        {
            // 이동 방향으로 회전하고 Y축 기준으로 90도 회전
            Quaternion targetRotation = Quaternion.LookRotation(movement) * Quaternion.Euler(0, 90, 0);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 10f); // 부드러운 회전
        }

        // 힘을 적용
        rb.AddForce(movement * PlayerState.power * Time.deltaTime, ForceMode.Force);

        // 최대 속도 제한
        if (rb.velocity.magnitude > PlayerState.maxSpeed)
        {
            rb.velocity = rb.velocity.normalized * PlayerState.maxSpeed;
        }
    }
}
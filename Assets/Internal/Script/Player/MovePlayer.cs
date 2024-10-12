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

    private void Start()
    {
        rb = this.GetComponent<Rigidbody>();
        child = transform.GetChild(0);

        // 키 매핑
        keyMappings = new Dictionary<string, KeyCode[]>()
        {
            { "Player1", new KeyCode[] { KeyCode.LeftArrow, KeyCode.RightArrow, KeyCode.UpArrow, KeyCode.DownArrow } },
            { "Player2", new KeyCode[] { KeyCode.A, KeyCode.D, KeyCode.W, KeyCode.S } }
        };

        // 각도 매핑
        rotationMappings = new Dictionary<string, float>()
        {
            { "Player1", 90f },
            { "Player2", 270f }
        };
    }
    /// <summary>
    /// 플레이어 이동 처리
    /// </summary>
    private void Update()
    {
        float moveHorizontal = 0;
        float moveVertical = 0;

        KeyCode[] controls = keyMappings[transform.name];

        // 키 입력 처리
        if (Input.GetKey(controls[0])) { moveHorizontal = -1; }
        if (Input.GetKey(controls[1])) { moveHorizontal = 1; }
        if (Input.GetKey(controls[2])) { moveVertical = 1; }
        if (Input.GetKey(controls[3])) { moveVertical = -1; }

        // 방향 벡터 계산
        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical).normalized;

        if (movement != Vector3.zero)
        {
            // 이동 방향을 기준으로 부모 축을 따라 회전
            float rotationAngle = rotationMappings[transform.name];
            Quaternion targetRotation = Quaternion.LookRotation(movement) * Quaternion.Euler(0, rotationAngle, 0); // 이동 방향으로 회전

            // 부모의 Y축을 기준으로 회전 조정
            child.rotation = Quaternion.Slerp(child.rotation, targetRotation, Time.deltaTime * 10f); // 부드러운 회전
        }

        // 힘을 적용
        rb.AddForce(movement * power * Time.deltaTime, ForceMode.Force);

        // 최대 속도 제한
        float slowdownRate = 0.97f; // 감속 비율
        if (rb.velocity.magnitude > maxSpeed) { rb.velocity *= slowdownRate; }
    }
}

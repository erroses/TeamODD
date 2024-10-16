using System.Collections.Generic;

using UnityEngine;

public class MovePlayer : MonoBehaviour
{
    private static readonly int MoveSide = Animator.StringToHash("MoveSide");
    private static readonly int MoveFront = Animator.StringToHash("MoveFront");
    private Rigidbody rb;
    private Transform child;

    private Dictionary<string, KeyCode[]> keyMappings;
    private Dictionary<string, float> rotationMappings;

    public float power = 1000000f;
    public float maxSpeed = 25.0f;
    public float moveRegion = 100f;
    public bool moveEnabled = false;
    public Animator animator;
    public SpriteRenderer spriteRenderer;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        child = transform.GetChild(0);
        keyMappings = new Dictionary<string, KeyCode[]>()
        {
            { "Player1", new[] { KeyCode.LeftArrow, KeyCode.RightArrow, KeyCode.UpArrow, KeyCode.DownArrow } },
            { "Player2", new[] { KeyCode.A, KeyCode.D, KeyCode.W, KeyCode.S } }
        };
        rotationMappings = new Dictionary<string, float>()
        {
            { "Player1", 90f },
            { "Player2", 270f }
        };
    }

    private void FixedUpdate()
    {
        if (!moveEnabled)
        {
            return;
        }

        int moveHorizontal = 0;
        int moveVertical = 0;

        KeyCode[] controls = keyMappings[transform.name];

        // Ű �Է� ó��
        if (Input.GetKey(controls[0]))
        {
            moveHorizontal = -1;
        }
        if (Input.GetKey(controls[1]))
        {
            moveHorizontal = 1;
        }
        if (Input.GetKey(controls[2]))
        {
            moveVertical = 1;
        }
        if (Input.GetKey(controls[3]))
        {
            moveVertical = -1;
        }

        int moveSide = moveVertical == 0 ? moveHorizontal : 0;
        int moveFront = moveVertical;

        animator.SetInteger(MoveSide, moveSide);
        animator.SetInteger(MoveFront, moveFront);

        if (moveSide < 0)
        {
            spriteRenderer.flipX = false;
        }
        else if (moveSide > 0)
        {
            spriteRenderer.flipX = true;
        }

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical).normalized;

        if (movement != Vector3.zero)
        {
            float rotationAngle = rotationMappings[transform.name];
            Quaternion targetRotation =
                Quaternion.LookRotation(movement) * Quaternion.Euler(0, rotationAngle, 0); // �̵� �������� ȸ��
            child.rotation = Quaternion.Slerp(child.rotation, targetRotation, Time.deltaTime * 10f); // �ε巯�� ȸ��
        }

        rb.AddForce(power * Time.deltaTime * movement, ForceMode.Force);

        if (rb.position.sqrMagnitude >= moveRegion * moveRegion)
        {
            rb.position = rb.position.normalized * moveRegion;
        }

        float slowdownRate = 0.97f;
        if (rb.velocity.magnitude > maxSpeed)
        {
            rb.velocity *= slowdownRate;
        }
    }
}

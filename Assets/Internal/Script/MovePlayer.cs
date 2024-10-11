using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class MovePlayer : MonoBehaviour
{
    private Rigidbody2D rb;
    public float power = 1000f;
    public float maxSpeed = 5.0f;

    private void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            rb.AddForce(Vector2.left * power * Time.deltaTime);
            if (rb.velocity.x < -maxSpeed)
            {
                rb.velocity = new Vector2(-maxSpeed, rb.velocity.y);
            }
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            rb.AddForce(Vector2.right * power * Time.deltaTime);
            if (rb.velocity.x > maxSpeed)
            {
                rb.velocity = new Vector2(maxSpeed, rb.velocity.y);
            }
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("ladder"))
        {
            if (Input.GetKey(KeyCode.UpArrow))
            {
                rb.AddForce(Vector2.up * power * Time.deltaTime);
                if (rb.velocity.x > maxSpeed)
                {
                    rb.velocity = new Vector2(maxSpeed, rb.velocity.x);
                }
            }
        }
    }

    void OnCollisionStay2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("panel"))
        {
            Collider2D otherCollider = other.collider; // 충돌한 오브젝트의 콜라이더 가져오기
            Collider2D thisCollider = GetComponent<Collider2D>(); // 이 오브젝트의 콜라이더 가져오기

            // 특정 키를 누를 때만 충돌을 무시
            if (Input.GetKey(KeyCode.UpArrow))
            {
                Physics2D.IgnoreCollision(otherCollider, thisCollider, true); // 충돌 무시
            }
        }
    }

    void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("panel"))
        {
            Collider2D otherCollider = other.collider;
            Collider2D thisCollider = GetComponent<Collider2D>();

            Physics2D.IgnoreCollision(otherCollider, thisCollider, false); // 충돌 다시 활성화
        }
    }
}

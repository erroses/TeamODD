using UnityEngine;
using System.Collections;

public class AttackCollision : MonoBehaviour
{
    private Rigidbody rb;
    Transform parent;
    GameObject parentObject;

    public float power = 7f;

    private void Start()
    {
        rb = this.GetComponent<Rigidbody>();
        parent = transform.parent;
        parentObject = parent.gameObject;
    }

    void OnTriggerStay(Collider other)
    {
        // 플레이어와 부딪혔을 경우
        if (other.gameObject.CompareTag("player") && other.gameObject != parentObject)
        {
            Vector3 KnockBackVelocity = Vector3.zero;

            if (parent.position.x > other.transform.position.x)
            {
                if (parent.position.z > other.transform.position.z)
                {
                    KnockBackVelocity = new Vector3(-power, 0f, -power); // 값 증가
                }
                else
                {
                    KnockBackVelocity = new Vector3(-power, 0f, power);
                }
            }
            else
            {
                if (parent.position.z > other.transform.position.z)
                {
                    KnockBackVelocity = new Vector3(power, 0f, -power); // 값 증가
                }
                else
                {
                    KnockBackVelocity = new Vector3(power, 0f, power);
                }
            }

            Rigidbody otherRb = other.gameObject.GetComponent<Rigidbody>();

            // 상대에게 넉백 효과 적용

            PlayerState playerState = otherRb.GetComponent<PlayerState>();
            playerState.maxSpeed = 1000f;
            otherRb.AddForce(KnockBackVelocity, ForceMode.Impulse);
            StartCoroutine(ResetMaxSpeed(playerState, 0.5f));
        }
    }

    void OnTriggerEnter(Collider other)
    {
        // 항아리와 부딪혔을 경우
        if (other.gameObject.CompareTag("jar"))
        {
            JarState jarState = other.gameObject.GetComponent<JarState>();
            if (parentObject.name == "Player1")
            {
                if (jarState.currentHealth < jarState.maxHealth)
                {
                    jarState.currentHealth++;
                    jarState.ChangeColor();
                }
            }
            else
            {
                if (jarState.currentHealth > 0)
                {
                    jarState.currentHealth--;
                    jarState.ChangeColor();
                }
            }
        }
    }
    IEnumerator ResetMaxSpeed(PlayerState playerState, float delay)
    {
        yield return new WaitForSeconds(delay);
        playerState.maxSpeed = 25f;
    }
}
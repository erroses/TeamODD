using UnityEngine;
using System.Collections;

public class AttackCollision : MonoBehaviour
{
    private PlayerData playerData1 = new PlayerData(1, "Player1");
    private PlayerData playerData2 = new PlayerData(2, "Player2");

    private Rigidbody rb;
    private Transform parent;
    private GameObject parentObject;

    public float power = 50f;

    private void Start()
    {
        rb = this.GetComponent<Rigidbody>();
        parent = transform.parent;
        parent = parent.parent;
        parentObject = parent.gameObject;
    }

    void OnTriggerStay(Collider other)
    {
        // 플레이어와 부딪혔을 경우
        if (other.gameObject.CompareTag("player") && other.gameObject != parentObject)
        {
            if(other.gameObject.name == "Player1") { playerData1.DamageCount++; }
            else { playerData2.DamageCount++; }

            Vector3 KnockBackVelocity = Vector3.zero;

            // 상대방과 부모 오브젝트의 위치를 비교하여 넉백 방향 결정
            float xDirection = parent.position.x > other.transform.position.x ? -power : power;
            float zDirection = parent.position.z > other.transform.position.z ? -power : power;

            // 넉백 벡터 설정
            KnockBackVelocity = new Vector3(xDirection, 0f, zDirection);

            Rigidbody otherRb = other.gameObject.GetComponent<Rigidbody>();

            // 상대에게 넉백 효과 적용
            otherRb.AddForce(KnockBackVelocity, ForceMode.Impulse);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("player") && other.gameObject != parentObject)
        {
            PlayerSound playerSound = other.gameObject.GetComponent<PlayerSound>();
            playerSound.audioSound();
        }

            // 항아리와 부딪혔을 경우
            if (other.gameObject.CompareTag("jar"))
        {
            JarState jarState = other.gameObject.GetComponent<JarState>();
            if (parentObject.name == "Player1")
            {
                if (jarState.currentHealth < jarState.maxHealth)
                {
                    jarState.currentHealth++;
                    jarState.ChangeColor(true);

                    if(jarState.currentHealth == jarState.maxHealth)
                    {
                        playerData1.DestroyCount++;
                    }
                }
            }
            else
            {
                if (jarState.currentHealth > 0)
                {
                    jarState.currentHealth--;
                    jarState.ChangeColor(false);

                    if (jarState.currentHealth == 0)
                    {
                        playerData2.DestroyCount++;
                    }
                }
            }
        }
    }
}
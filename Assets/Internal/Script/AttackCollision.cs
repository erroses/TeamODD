using UnityEngine;

public class AttackCollision : MonoBehaviour
{
    private PlayerData[] playerData = new PlayerData[2];

    private Transform parent;
    private GameObject parentObject;

    public float power = 50f;

    private void Start()
    {
        parent = transform.parent;
        parent = parent.parent;
        parentObject = parent.gameObject;

        // 플레이어 데이터 초기화
        for (int i = 0; i < 2; i++)
        {
            playerData[i] = new PlayerData(i + 1, $"Player{i + 1}");
        }
    }

    private void OnTriggerStay(Collider other)
    {
        // 플레이어와 부딪혔을 경우
        if (other.gameObject.CompareTag("player") && other.gameObject != parentObject)
        {
            int playerIndex = transform.name.Equals("Player1") ? 0 : 1; // 이름에 따라 인덱스 결정
            playerData[playerIndex].DamageCount++;

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

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("player") && other.gameObject != parentObject)
        {
            PlayerSound playerSound = other.gameObject.GetComponent<PlayerSound>();
            playerSound.audioSound();
            bool isPlayer1 = parent.name.Equals("Player1");
            if (isPlayer1)
            {
                GameStatistics.Instance.Player1AttackCount++;
            }
            else
            {
                GameStatistics.Instance.Player2AttackCount++;
            }
        }

        // 항아리와 부딪혔을 경우
        if (other.gameObject.CompareTag("jar"))
        {
            JarState jarState = other.gameObject.GetComponent<JarState>();

            int playerIndex = parent.name.Equals("Player1") ? 0 : 1; // 이름에 따라 인덱스 결정
            bool isRepair = parent.name.Equals("Player1"); // 수리 여부 결정
            int healthChange = isRepair ? 1 : -1;
            int previousHealth = jarState.currentHealth;

            // 체력 최대최소 범위 지정(그 안에서만)
            // jarState.currentHealth = Mathf.Clamp(jarState.currentHealth + healthChange, 0, jarState.maxHealth);
            jarState.SetHealthPoint(Mathf.Clamp(jarState.currentHealth + healthChange, 0, jarState.maxHealth));

            // 체력이 변했을 경우
            if (jarState.currentHealth != previousHealth)
            {
                jarState.UpdateJarState(isRepair);

                if (jarState.currentHealth == jarState.maxHealth) { playerData[0].DestroyCount++; }
                if (jarState.currentHealth == 0) { playerData[1].DestroyCount++; }
            }
        }
    }
}

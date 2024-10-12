using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private PlayerData[] playerData = new PlayerData[2];
    private Transform child;
    private GameObject childObject;

    private Dictionary<string, KeyCode> keyMappings;

    public float attackCooldown = 0.3f; // 공격 쿨타임 (N초)
    public bool canAttack = true; // 공격 가능 여부
    private void Start()
    {
        child = transform.GetChild(0);
        childObject = child.gameObject;

        // 공격 키 매핑
        keyMappings = new Dictionary<string, KeyCode>()
        {
            { "Player1", KeyCode.P },
            { "Player2", KeyCode.L },
        };

        // 플레이어 데이터 초기화
        for (int i = 0; i < 2; i++) {
            playerData[i] = new PlayerData(i + 1, $"Player{i + 1}");
            playerData[i].OnAttackCountIncrease.AddListener(count =>
            {
                StartCoroutine(attackObject());
            });
        }
    }

    /// <summary>
    /// 플레이어의 공격 입력 처리
    /// </summary>
    private void Update()
    {
        if (Input.GetKey(keyMappings[transform.name]) && canAttack)
        {
            int playerIndex = transform.name.Equals("Player1") ? 0 : 1; // 이름에 따라 인덱스 결정
            playerData[playerIndex].AttackCount++;
        }
    }

    /// <summary>
    /// 공격 범위를 활성화하고 쿨타임을 관리
    /// </summary>
    /// <returns></returns>
    IEnumerator attackObject()
    {
        canAttack = false; // 공격 불가능 상태로 설정
        childObject.SetActive(true); // 공격 범위 활성화
        yield return new WaitForSeconds(0.1f); // 잠시 대기
        childObject.SetActive(false); // 공격 범위 비활성화
        yield return new WaitForSeconds(attackCooldown); // 공격 쿨타임 대기
        canAttack = true; // 공격 가능 상태로 설정
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private PlayerData[] playerData = new PlayerData[2];
    private Transform child;
    private GameObject childObject;

    private Dictionary<string, KeyCode> keyMappings;

    public float attackCooldown = 0.3f; // ���� ��Ÿ�� (N��)
    public bool canAttack = true; // ���� ���� ����
    private void Start()
    {
        child = transform.GetChild(0);
        childObject = child.gameObject;

        // ���� Ű ����
        keyMappings = new Dictionary<string, KeyCode>()
        {
            { "Player1", KeyCode.Return },
            { "Player2", KeyCode.Space },
        };

        // �÷��̾� ������ �ʱ�ȭ
        for (int i = 0; i < 2; i++)
        {
            playerData[i] = new PlayerData(i + 1, $"Player{i + 1}");
            playerData[i].OnAttackCountIncrease.AddListener(count =>
            {
                StartCoroutine(attackObject());
            });
        }
    }

    /// <summary>
    /// �÷��̾��� ���� �Է� ó��
    /// </summary>
    private void Update()
    {
        if (Input.GetKey(keyMappings[transform.name]) && canAttack)
        {
            int playerIndex = transform.name.Equals("Player1") ? 0 : 1; // �̸��� ���� �ε��� ����
            playerData[playerIndex].AttackCount++;
        }
    }

    /// <summary>
    /// ���� ������ Ȱ��ȭ�ϰ� ��Ÿ���� ����
    /// </summary>
    /// <returns></returns>
    IEnumerator attackObject()
    {
        canAttack = false; // ���� �Ұ��� ���·� ����
        childObject.SetActive(true); // ���� ���� Ȱ��ȭ
        yield return new WaitForSeconds(0.1f); // ��� ���
        childObject.SetActive(false); // ���� ���� ��Ȱ��ȭ
        yield return new WaitForSeconds(attackCooldown); // ���� ��Ÿ�� ���
        canAttack = true; // ���� ���� ���·� ����
    }
}

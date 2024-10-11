using System.Collections;
using UnityEngine;

public class PlayerAttack1 : MonoBehaviour
{
    PlayerData playerData = new PlayerData(1, "Player1");
    Transform child;
    GameObject childObject;

    public float attackCooldown = 0.3f; // ���� ��Ÿ�� (N��)
    public bool canAttack = true; // ���� ���� ����

    void Start()
    {
        child = transform.GetChild(0);
        childObject = child.gameObject;

        playerData.OnAttackCountIncrease.AddListener(count =>
        {
            StartCoroutine(attackObject());
        });
    }

    // ���� ���� �ð� ���� ����
    void Update()
    {
        // ����Ű(���� ����)
        if (Input.GetKey(KeyCode.P) && canAttack)
        {
            // ���� ���� �ѱ�
            playerData.AttackCount++;
        }
    }

    IEnumerator attackObject()
    {
        canAttack = false;
        childObject.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        childObject.SetActive(false);
        yield return new WaitForSeconds(attackCooldown);
        canAttack = true;
    }
}
using System.Collections;
using UnityEngine;

public class PlayerAttack2 : MonoBehaviour
{
    Transform child;
    GameObject childObject;

    public float attackCooldown = 0.3f; // ���� ��Ÿ�� (N��)
    public bool canAttack = true; // ���� ���� ����

    void Start()
    {
        child = transform.GetChild(0);
        childObject = child.gameObject;
    }

    // ���� ���� �ð� ���� ����
    void Update()
    {
        // ����Ű(���� ����)
        if (Input.GetKey(KeyCode.L) && canAttack)
        {
            // ���� ���� �ѱ�
            StartCoroutine(attackObject());
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
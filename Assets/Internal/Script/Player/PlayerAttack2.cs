using System.Collections;
using UnityEngine;

public class PlayerAttack2 : MonoBehaviour
{
    Transform child;
    GameObject childObject;

    public float attackCooldown = 0.3f; // 공격 쿨타임 (N초)
    public bool canAttack = true; // 공격 가능 여부

    void Start()
    {
        child = transform.GetChild(0);
        childObject = child.gameObject;
    }

    // 추후 공격 시간 제한 지정
    void Update()
    {
        // 공격키(임의 지정)
        if (Input.GetKey(KeyCode.L) && canAttack)
        {
            // 공격 범위 켜기
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
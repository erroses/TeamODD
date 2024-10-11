using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackRangeMove : MonoBehaviour
{
    Transform parent;
    GameObject parentObject;

    void Start()
    {

        parent = transform.parent;
        parentObject = parent.gameObject;
    }

    void Rotation()
    {
        // 추후 만들거임
    }
}

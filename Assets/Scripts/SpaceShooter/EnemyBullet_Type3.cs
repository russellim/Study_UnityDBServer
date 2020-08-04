using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet_Type3 : EnemyBullet
{
    Transform Target;

    private void Awake()
    {
        Target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void OnEnable()
    {
        Vector2 derection = Target.position - transform.position;
        float angle = Mathf.Atan2(derection.y, derection.x) * Mathf.Rad2Deg + 90f;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
    }
}

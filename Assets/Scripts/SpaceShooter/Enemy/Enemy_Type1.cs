using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Type1 : Enemy
{
    public override void OnEnable()
    {
        base.OnEnable();
        StartCoroutine(Attack());
    }
}

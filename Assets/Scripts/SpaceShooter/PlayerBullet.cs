﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    public float Speed;
    public float DestroyTime = 2f;

    private void OnEnable()
    {
        StartCoroutine(Disable());
    }

    private void Update()
    {
        transform.Translate(Vector3.up * Speed * PlayerManager.Instance.MultiBulletSpeed * Time.deltaTime);
    }

    IEnumerator Disable()
    {
        yield return new WaitForSeconds(DestroyTime);
        ObjectPool.Instance.PushToPool(gameObject.name, gameObject);
    }
}
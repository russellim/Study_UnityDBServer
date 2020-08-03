using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public float Speed;
    public float DestroyTime = 2f;

    private void OnEnable()
    {
        StartCoroutine(Disable());
    }

    private void Update()
    {
        transform.Translate(Vector2.down * Speed * Time.deltaTime);
    }

    IEnumerator Disable()
    {
        yield return new WaitForSeconds(DestroyTime);
        ObjectPool.Instance.PushToPool(gameObject.name, gameObject);
    }
}

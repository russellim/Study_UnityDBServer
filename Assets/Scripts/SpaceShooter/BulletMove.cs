using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMove : MonoBehaviour
{
    public float Speed;

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
        yield return new WaitForSeconds(2f);
        ObjectPool.Instance.PushToPool(gameObject.name, gameObject);
    }
}

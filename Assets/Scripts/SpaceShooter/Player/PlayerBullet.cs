using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    public float Speed;
    public float DestroyTime = 2f;

    private void Update()
    {
        transform.Translate(Vector3.up * Speed * Player.Instance.MultiBulletSpeed * Time.deltaTime);

        if (transform.position.y >= -GameManager.Instance.OutPositionY)
        {
            Disable();
        }
    }

    void Disable()
    {
        ObjectPool.Instance.PushToPool(gameObject.name, gameObject);
    }
}

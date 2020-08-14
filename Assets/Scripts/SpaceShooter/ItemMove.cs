using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemMove : MonoBehaviour
{
    public float Speed;

    private void Update()
    {
        transform.Translate(Vector2.down * Speed * Time.deltaTime);

        if (transform.position.y <= GameManager.Instance.OutPositionY)
        {
            Disable();
        }
    }

    void Disable()
    {
        ObjectPool.Instance.PushToPool(gameObject.name, gameObject);
    }
}

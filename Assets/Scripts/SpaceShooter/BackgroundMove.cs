using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMove : MonoBehaviour
{
    public float Speed;

    private void Update()
    {
        transform.Translate(Vector3.down * Speed * Time.deltaTime);

        if(transform.position.y <= -10.22f)
        {
            transform.position = Vector3.zero;
        }
    }
}

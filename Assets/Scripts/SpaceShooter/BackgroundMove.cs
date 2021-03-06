﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMove : MonoBehaviour
{
    public float Speed;
    public SpriteRenderer[] renderers;
    public Sprite[] sprites;

    private void Start()
    {
        int num = Random.Range(0, sprites.Length);
        for(int i=0; i< renderers.Length; ++i)
        {
            renderers[i].sprite = sprites[num];
        }
    }

    private void Update()
    {
        transform.Translate(Vector3.down * Speed * Time.deltaTime);

        if(transform.position.y <= -10.22f)
        {
            transform.position = Vector3.zero;
        }
    }
}

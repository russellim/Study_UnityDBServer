using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Type2 : Enemy
{
    [Header("Type2")]
    public Enemy_Type2s Mother;
    public bool ImAttack = false;
    public Transform[] Sockets;

    public override void OnEnable()
    {
        CurrentHP = SetHP;
        IsDie = false;
        StartCoroutine(Move());
        StartCoroutine(Attack());
    }

    public override IEnumerator Move()
    {
        yield return null;
    }

    public override IEnumerator Attack()
    {
        while (!IsDie)
        {
            if(ImAttack)
            {
                CreateBullet(0);
                CreateBullet(1);
                ImAttack = false;
            }
            yield return null;
        }
    }

    void CreateBullet(int SocketNumber)
    {
        GameObject BulletOB;
        BulletOB = ObjectPool.Instance.PopFromPool(BulletName);
        BulletOB.transform.SetPositionAndRotation(Sockets[SocketNumber].position, Sockets[SocketNumber].rotation);
        BulletOB.SetActive(true);
    }

    private void OnDisable()
    {
        if(IsDie)
        {
            Mother.LiveType2s.Remove(this);
        }
    }
}

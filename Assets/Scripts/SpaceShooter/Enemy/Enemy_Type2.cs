using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Type2 : Enemy
{
    [Header("Type2")]
    public Enemy_Type2s Mother;
    public bool ImAttack = false;
    public Transform[] Sockets;

    public override IEnumerator Move()
    {
        yield return null;
    }

    new public void Attack()
    {
        CreateBullet(0);
        CreateBullet(1);
    }

    void CreateBullet(int SocketNumber)
    {
        GameObject BulletOB;
        BulletOB = ObjectPool.Instance.PopFromPool(BulletName);
        BulletOB.transform.SetPositionAndRotation(Sockets[SocketNumber].position, Sockets[SocketNumber].rotation);
        BulletOB.SetActive(true);
    }

    public override IEnumerator Die()
    {
        Mother.LiveType2s.Remove(this);
        return base.Die();
    }

    public override void Disable()
    {
        Mother.LiveType2s.Remove(this);
    }
}
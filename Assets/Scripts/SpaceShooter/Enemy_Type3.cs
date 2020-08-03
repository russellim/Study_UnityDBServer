using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Type3 : Enemy
{
    public Vector2[] WayPoints;

    public override void OnEnable()
    {
        CurrentHP = SetHP;
        IsDie = false;

        if((transform.position.x > 0 && WayPoints[0].x < 0) ||
            (transform.position.x < 0 && WayPoints[0].x > 0))
        {
            for(int i=0; i<WayPoints.Length; ++i)
            {
                WayPoints[i].x *= -1;
            }
        }

        StartCoroutine(Move());
    }

    override public IEnumerator Move()
    {
        for(int i=0; i< WayPoints.Length; ++i)
        {
            while(transform.position.x != WayPoints[i].x)
            {
                transform.position = Vector2.MoveTowards(transform.position, WayPoints[i], Speed * Time.deltaTime);
                yield return null;
            }
            Attack();
            yield return new WaitForSeconds(1.5f);
        }

        ObjectPool.Instance.PushToPool(gameObject.name, gameObject);
    }

    new void Attack()
    {
        GameObject BulletOB;
        BulletOB = ObjectPool.Instance.PopFromPool(BulletName);
        BulletOB.transform.SetPositionAndRotation(Socket.position, Socket.rotation);
        BulletOB.SetActive(true);
    }
}

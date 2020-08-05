using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Type3 : Enemy
{
    [Header("Type3")]
    public Vector2[] WayPoints;

    public override void OnEnable()
    {
        if((transform.position.x > 0 && WayPoints[0].x < 0) ||
            (transform.position.x < 0 && WayPoints[0].x > 0))
        {
            for(int i=0; i<WayPoints.Length; ++i)
            {
                WayPoints[i].x *= -1;
            }
        }
        base.OnEnable();
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
            if(!IsDie) Attack();
            yield return new WaitForSeconds(0.5f);
        }

        Disable();
    }

    new void Attack()
    {
        GameObject BulletOB;
        BulletOB = ObjectPool.Instance.PopFromPool(BulletName);
        BulletOB.transform.SetPositionAndRotation(Socket.position, Socket.rotation);
        BulletOB.SetActive(true);
    }
}

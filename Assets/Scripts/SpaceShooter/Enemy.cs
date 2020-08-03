using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // 공통.
    // HP (데미지, 죽음).
    // 공격
    // 상태.

    public int CurrentHP;
    public int SetHP = 1;

    public float Speed;
    public float DestroyTime = 5f;

    public string BulletName;
    public Transform Socket;
    public float RunningFire = 1.5f;


    public bool IsDie = false;

    virtual public void OnEnable()
    {
        CurrentHP = SetHP;
        IsDie = false;
        StartCoroutine(Move());
        StartCoroutine(Attack());
        StartCoroutine(Disable());
    }

    virtual public IEnumerator Move()
    {
        while(!IsDie)
        {
            transform.Translate(Vector3.down * Speed * Time.deltaTime);
            yield return null;
        }
    }

    virtual public IEnumerator Attack()
    {
        yield return new WaitForSeconds(0.3f);
        while (!IsDie)
        {
            GameObject BulletOB;
            BulletOB = ObjectPool.Instance.PopFromPool(BulletName);
            BulletOB.transform.SetPositionAndRotation(Socket.position, Socket.rotation);
            BulletOB.SetActive(true);
            yield return new WaitForSeconds(RunningFire);
        }
    }

    IEnumerator Disable()
    {
        yield return new WaitForSeconds(DestroyTime);
        ObjectPool.Instance.PushToPool(gameObject.name, gameObject);
    }
}

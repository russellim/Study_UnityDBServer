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
    public Transform BulletSocket;
    public float RunningFire = 1.5f;


    bool IsDie = false;

    private void OnEnable()
    {
        CurrentHP = SetHP;
        IsDie = false;
        StartCoroutine(Move());
        StartCoroutine(Attack());
        StartCoroutine(Disable());
    }

    IEnumerator Move()
    {
        while(!IsDie)
        {
            transform.Translate(Vector3.down * Speed * Time.deltaTime);
            yield return null;
        }
    }

    IEnumerator Attack()
    {
        yield return new WaitForSeconds(0.3f);
        while (!IsDie)
        {
            GameObject BulletOB;
            BulletOB = ObjectPool.Instance.PopFromPool(BulletName);
            BulletOB.transform.position = BulletSocket.position;
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

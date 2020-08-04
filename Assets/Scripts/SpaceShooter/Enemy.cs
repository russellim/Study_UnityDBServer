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

    public string BulletName;
    public Transform Socket;
    public float RunningFire = 1.5f;

    public SpriteRenderer spriteRenderer;
    public Collider2D col;
    public string ExplosionParticleName = "explosion_enemy";
    public bool IsDie = false;

    virtual public void OnEnable()
    {
        IsDie = false;
        spriteRenderer.enabled = true;
        col.enabled = true;
        CurrentHP = SetHP;
        StartCoroutine(Move());
    }

    virtual public IEnumerator Move()
    {
        while(!IsDie)
        {
            transform.Translate(Vector3.down * Speed * Time.deltaTime);

            if(transform.position.y <= GameManager.Instance.OutPositionY)
            {
                Disable();
            }
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag.Equals("PlayerBullet"))
        {
            CurrentHP--;
            collision.gameObject.SetActive(false);

            if (CurrentHP <= 0)
            {
                // Die.
                StartCoroutine(Die());
            }
        }
    }

    IEnumerator Die()
    {
        IsDie = true;
        spriteRenderer.enabled = false;
        col.enabled = false;

        ParticleSystem ExplosionParticle;
        ExplosionParticle = ObjectPool.Instance.PopFromPool(ExplosionParticleName).GetComponent<ParticleSystem>();
        ExplosionParticle.gameObject.transform.position = transform.position;
        ExplosionParticle.gameObject.SetActive(true);
        ExplosionParticle.Play();

        yield return new WaitForSeconds(1f);
        Disable();
    }

    virtual public void Disable()
    {
        ObjectPool.Instance.PushToPool(gameObject.name, gameObject);
    }
}

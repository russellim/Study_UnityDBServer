using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // 공통.
    // HP (데미지, 죽음).
    // 공격
    // 상태.
    public int Score = 10;

    public int CurrentHP;
    public int SetHP = 1;

    public float Speed;

    public string BulletName;
    public Transform Socket;
    public float RunningFire = 1.5f;

    protected float MultiSpeed;
    protected float MultiRunningFire;


    SpriteRenderer spriteRenderer;
    Collider2D col;
    const string ExplosionParticleName = "explosion_enemy";
    protected bool IsDie = false;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        col = GetComponent<Collider2D>();
    }

    virtual public void OnEnable()
    {
        IsDie = false;
        spriteRenderer.enabled = true;
        col.enabled = true;
        CurrentHP = SetHP;

        MultiSpeed = GameManager.Instance.MultiEnemySpeed;
        MultiRunningFire = GameManager.Instance.MultiEnemyRunningFire;

        StartCoroutine(Move());
    }

    virtual public IEnumerator Move()
    {
        while(!IsDie)
        {
            transform.Translate(Vector3.down * Speed * MultiSpeed * Time.deltaTime);

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
            yield return new WaitForSeconds(RunningFire * MultiRunningFire);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayerBullet"))
        {
            CurrentHP--;
            collision.gameObject.SetActive(false);

            if (CurrentHP <= 0)
            {
                // Die.
                StartCoroutine(Die());
            }
        }
        else if (!Player.Instance.IsRevivaling && collision.CompareTag("Player"))
        {
            Player.Instance.GetDamage();
            StartCoroutine(Die());
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

        GameManager.Instance.PlusScore(Score);
        Player.Instance.ExpUp();

        yield return new WaitForSeconds(1f);
        Disable();
    }

    virtual public void Disable()
    {
        ObjectPool.Instance.PushToPool(gameObject.name, gameObject);
    }
}

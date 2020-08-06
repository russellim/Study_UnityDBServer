using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Singleton<Player>
{
    public int FullHP = 5;
    public int CurrentHP = 3;
    public int PowerUp = 1;
    public float MultiBulletSpeed = 1f;
    public float MultiRunningFire = 1f;


    public SpriteRenderer spriteRenderer;
    public Collider2D col;
    public ParticleSystem ExplosionParticle;
    public bool IsDie = false;
    public bool IsRevivaling = false;
    public bool IsExplosion = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!IsRevivaling && !IsDie && (collision.CompareTag("EnemyBullet") || collision.CompareTag("Enemy")))
        {
            CurrentHP--;
            collision.gameObject.SetActive(false);
            IsExplosion = true;

            if (CurrentHP <= 0)
            {
                // Die.
                StartCoroutine(Die());
            }
            else
            {
                StartCoroutine(Revival());
            }
        }
    }

    IEnumerator Revival()
    {
        IsRevivaling = true;
        spriteRenderer.enabled = false;
        col.enabled = false;
        ExplosionParticle.Play();

        yield return new WaitForSeconds(2f);
        IsExplosion = false;
        transform.position = GameManager.Instance.PlayerStartPosition;
        spriteRenderer.enabled = true;
        col.enabled = true;
        float RevivalTime = 3f;
        while(RevivalTime >= 0f)
        {
            spriteRenderer.color = new Color(1f, 1f, 1f, 0.3f);
            yield return new WaitForSeconds(0.2f);
            spriteRenderer.color = new Color(1f, 1f, 1f, 0.7f);
            yield return new WaitForSeconds(0.2f);
            RevivalTime -= 0.4f;
        }
        spriteRenderer.color = Color.white;
        IsRevivaling = false;
    }

    IEnumerator Die()
    {
        IsDie = true;
        spriteRenderer.enabled = false;
        col.enabled = false;

        ExplosionParticle.Play();

        yield return new WaitForSeconds(2f);

        UIManager.Instance.DisplayGameOverUI();
        GameManager.Instance.SaveHighScore();
    }

}

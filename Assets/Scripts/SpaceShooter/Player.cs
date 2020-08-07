using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Singleton<Player>
{
    private PlayerLevel CurrentNeedExp;
    private int Level = 1;
    private int Exp = 0;

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

    private void Start()
    {
        SetCurrentNeedExp(1);
    }

    public void SetCurrentNeedExp(int Level)
    {
        CurrentNeedExp = JSONManager.Instance.PlayerLevels[Level - 1];
    }
    void LevelUp()
    {
        Level++;
        UIManager.Instance.UpdateLevelText(Level);
        MultiBulletSpeed += 0.05f;
        MultiRunningFire -= 0.05f;
        if (Level == 11)
        {
            UIManager.Instance.UpdateExpProgress(1, 1);
            return;
        }
        SetCurrentNeedExp(Level);
        Exp = 0;
        UIManager.Instance.UpdateExpProgress(Exp, CurrentNeedExp.Exp);
    }
    public void ExpUp()
    {
        if (Level == 11) return;
            
        Exp++;
        if (Exp < CurrentNeedExp.Exp)
        {
            UIManager.Instance.UpdateExpProgress(Exp, CurrentNeedExp.Exp);
        }
        else
        {
            LevelUp();
        }
    }


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

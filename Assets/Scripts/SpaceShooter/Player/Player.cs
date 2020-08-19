using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Singleton<Player>
{
    private PlayerLevel CurrentNeedExp;
    public int Level = 1;
    private int Exp = 0;

    public int FullHP = 5;
    public int CurrentHP = 3;
    public int SpecialCount = 1;
    public int PowerUp = 1;
    public float MultiBulletSpeed = 1f;
    public float MultiRunningFire = 1f;

    public SpriteRenderer spriteRenderer;
    public Collider2D col;
    public ParticleSystem ExplosionParticle;
    public AudioSource ExplosionSound;
    public bool IsDie = false;
    public bool IsRevivaling = false;
    public bool IsExplosion = false;

    public AudioSource PowerUpSound;
    public AudioSource HeartUpSound;

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
        GameManager.Instance.MultiEnemySpeed += 0.05f;
        GameManager.Instance.MultiEnemyRunningFire -= 0.05f;
        GameManager.Instance.MultiEnemySpawnTime -= 0.05f;
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
        if (!IsRevivaling && !IsDie && collision.CompareTag("EnemyBullet"))
        {
            collision.gameObject.SetActive(false);
            GetDamage();
        }

        if (!IsDie && collision.CompareTag("Stat"))
        {
            ObjectPool.Instance.PushToPool("StatItem", collision.gameObject);
            PowerUpSound.Play();
            if (Random.Range(0, 2) == 0)
            {
                if (PowerUp == 1) GameManager.Instance.PlusScore(-10);
                else PowerUp--;
            }
            else
            {
                if (PowerUp == 3) GameManager.Instance.PlusScore(30);
                else PowerUp++;
            }
        }

        if(!IsDie && collision.CompareTag("Heart"))
        {
            ObjectPool.Instance.PushToPool("HeartItem", collision.gameObject);
            HeartUpSound.Play();
            if (CurrentHP == 5)
            {
                GameManager.Instance.PlusScore(20);
            }
            else
            {
                GetHP();
            }
        }

        if (!IsDie && collision.CompareTag("Special"))
        {
            ObjectPool.Instance.PushToPool("SpecialItem", collision.gameObject);
            HeartUpSound.Play();
            if (CurrentHP == 3)
            {
                GameManager.Instance.PlusScore(20);
            }
            else
            {
                GetSpecial();
            }
        }
    }

    public void GetDamage()
    {
        CurrentHP--;
        IsExplosion = true;
        UIManager.Instance.UpdateHPUI(CurrentHP, false);

        if (CurrentHP <= 0)
        {
            // Die.
            StartCoroutine(Die());
        }
        else
        {
            PowerUp = 1;
            StartCoroutine(Revival());
        }
    }

    void GetHP()
    {
        CurrentHP++;
        UIManager.Instance.UpdateHPUI(CurrentHP, true);
    }
    
    void GetSpecial()
    {
        SpecialCount++;
        UIManager.Instance.UpdateSpecialUI(SpecialCount, true);
    }

    IEnumerator Revival()
    {
        IsRevivaling = true;
        spriteRenderer.enabled = false;
        col.enabled = false;
        ExplosionParticle.Play();
        ExplosionSound.Play();

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
        UIManager.Instance.SpecialButton.interactable = false;

        ExplosionParticle.Play();

        UIManager.Instance.PauseButton.SetActive(false);

        yield return new WaitForSeconds(2f);

        UIManager.Instance.DisplayGameOverUI();
        GameManager.Instance.SaveHighScore();
    }

}

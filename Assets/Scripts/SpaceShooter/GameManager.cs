using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public int Score = 0;
    int HighScore = 0;

    public float PlayTime = 0.0f;

    public float OutPositionY = -7f;
    public Vector2 PlayerStartPosition = new Vector2(0f, -3.74f);
    public float MultiEnemySpeed = 1f;
    public float MultiEnemyRunningFire = 1f;
    public float MultiEnemySpawnTime = 1f;

    public float StatItemTime = 0f;
    public float HeartItemTime = 0f;

    // 게임상태.
    public bool IsPause = false;

    private void Start()
    {
        HighScore = LoadHighScore();
        UIManager.Instance.UpdateHighScoreUI(HighScore);

        StartCoroutine(SpawnEnemy());
        StartCoroutine(CheckTime());
        StartCoroutine(HeartItemCoolTime());
        StartCoroutine(StatItemCoolTime());
    }

    IEnumerator SpawnEnemy()
    {
        int num;
        while(!Player.Instance.IsDie)
        {
            num = Random.Range(1, 3 + 1);
            switch (num)
            {
                case 1:
                    CreateEnemy("Enemy_Type1", -3.23f, 1f);
                    CreateEnemy("Enemy_Type1", -1f, 3.23f);
                    yield return new WaitForSeconds(Random.Range(3f * MultiEnemySpawnTime, 4f * MultiEnemySpawnTime));
                    break;
                case 2:
                    CreateEnemy("Enemy_Type2s", -1f, 1f);
                    yield return new WaitForSeconds(Random.Range(3f * MultiEnemySpawnTime, 4f * MultiEnemySpawnTime));
                    break;
                case 3:
                    num = Random.Range(0, 2) == 0 ? 1 : -1;
                    CreateEnemy("Enemy_Type3", new Vector2(num * 4.36f, -0.45f));
                    yield return new WaitForSeconds(0.5f * MultiEnemySpawnTime);
                    CreateEnemy("Enemy_Type3", new Vector2(num * 4.36f, -0.45f));
                    yield return new WaitForSeconds(0.5f * MultiEnemySpawnTime);
                    CreateEnemy("Enemy_Type3", new Vector2(num * 4.36f, -0.45f));
                    yield return new WaitForSeconds(Random.Range(2f * MultiEnemySpawnTime, 3f * MultiEnemySpawnTime));
                    break;
            }
        }
    }

    void CreateEnemy(string name, float MinPos, float MaxPos)
    {
        GameObject EnemyOB;
        EnemyOB = ObjectPool.Instance.PopFromPool(name);
        EnemyOB.transform.position = new Vector2(Random.Range(MinPos, MaxPos), 6.9f);
        EnemyOB.SetActive(true);
    }
    void CreateEnemy(string name, Vector2 pos)
    {
        GameObject EnemyOB;
        EnemyOB = ObjectPool.Instance.PopFromPool(name);
        EnemyOB.transform.position = new Vector2(pos.x, pos.y);
        EnemyOB.SetActive(true);
    }

    public void PlusScore(int AddScore)
    {
        Score += AddScore;
        UIManager.Instance.UpdatePlayerScoreUI(Score);
        if(HighScore < Score)
        {
            HighScore = Score;
            UIManager.Instance.UpdateHighScoreUI(HighScore);
        }
    }

    public void SaveHighScore()
    {
        if (HighScore == Score)
        {
            PlayerPrefs.SetInt("HighScore", HighScore);
        }
    }
    int LoadHighScore()
    {
        return PlayerPrefs.GetInt("HighScore", 0);
    }

    private void OnApplicationPause(bool pause)
    {
        if(!Player.Instance.IsDie && pause)
        {
            UIManager.Instance.OnClickPauseButton();
        }
    }

    public void SpawnItem(GameObject root)
    {
        if (StatItemTime <= 0f)
        {
            GameObject ItemOB;
            ItemOB = ObjectPool.Instance.PopFromPool("StatItem");
            ItemOB.transform.SetPositionAndRotation(root.transform.position, root.transform.rotation);
            ItemOB.SetActive(true);
            StartCoroutine(StatItemCoolTime());
            return;
        }

        if (HeartItemTime <= 0f)
        {
            GameObject ItemOB;
            if(Random.Range(0,2) == 0) ItemOB = ObjectPool.Instance.PopFromPool("HeartItem");
            else ItemOB = ObjectPool.Instance.PopFromPool("SpecialItem");
            ItemOB.transform.SetPositionAndRotation(root.transform.position, root.transform.rotation);
            ItemOB.SetActive(true);
            StartCoroutine(HeartItemCoolTime());
        }
    }

    public IEnumerator StatItemCoolTime()
    {
        StatItemTime = Random.Range(10, 15);
        while (StatItemTime > 0f)
        {
            StatItemTime -= 1f;
            yield return new WaitForSeconds(1f);
        }
    }
    public IEnumerator HeartItemCoolTime()
    {
        HeartItemTime = Random.Range(25, 30);
        while(HeartItemTime > 0f)
        {
            HeartItemTime -= 1f;
            yield return new WaitForSeconds(1f);
        }
    }

    IEnumerator CheckTime()
    {
        while(!Player.Instance.IsDie)
        {
            PlayTime += Time.deltaTime;
            yield return null;
        }
    }
}

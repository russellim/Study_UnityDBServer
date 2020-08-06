using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public int Score = 0;
    int HighScore = 0;

    public float OutPositionY = -7f;
    public Vector2 PlayerStartPosition = new Vector2(0f, -3.74f);

    private void Start()
    {
        HighScore = LoadHighScore();
        UIManager.Instance.UpdateHighScoreUI(HighScore);

        StartCoroutine(SpawnEnemy());
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
                    yield return new WaitForSeconds(Random.Range(3f, 4f));
                    break;
                case 2:
                    CreateEnemy("Enemy_Type2s", -1f, 1f);
                    yield return new WaitForSeconds(Random.Range(3f, 4f));
                    break;
                case 3:
                    num = Random.Range(0, 2) == 0 ? 1 : -1;
                    CreateEnemy("Enemy_Type3", new Vector2(num * 4.36f, -0.45f));
                    yield return new WaitForSeconds(0.5f);
                    CreateEnemy("Enemy_Type3", new Vector2(num * 4.36f, -0.45f));
                    yield return new WaitForSeconds(0.5f);
                    CreateEnemy("Enemy_Type3", new Vector2(num * 4.36f, -0.45f));
                    yield return new WaitForSeconds(Random.Range(2f, 3f));
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

}

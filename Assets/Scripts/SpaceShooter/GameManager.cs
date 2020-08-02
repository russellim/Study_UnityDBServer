using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    int score = 0;

    private void Start()
    {
        StartCoroutine(SpawnEnemy());
    }

    IEnumerator SpawnEnemy()
    {
        while(!PlayerManager.Instance.IsDie)
        {
            CreateEnemy("Enemy_Type1", -3.23f, 1f);
            CreateEnemy("Enemy_Type1", -1f, 3.23f);
            yield return new WaitForSeconds(Random.Range(3f, 4f));
        }
    }

    void CreateEnemy(string name, float MinPos, float MaxPos)
    {
        GameObject EnemyOB;
        EnemyOB = ObjectPool.Instance.PopFromPool(name);
        EnemyOB.transform.position = new Vector2(Random.Range(MinPos, MaxPos), 6.9f);
        EnemyOB.SetActive(true);
    }
}

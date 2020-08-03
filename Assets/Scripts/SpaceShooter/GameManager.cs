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
        int num;
        while(!PlayerManager.Instance.IsDie)
        {
            num = Random.Range(1, 2 + 1);
            switch (num)
            {
                case 1:
                    CreateEnemy("Enemy_Type1", -3.23f, 1f);
                    CreateEnemy("Enemy_Type1", -1f, 3.23f);
                    break;
                case 2:
                    CreateEnemy("Enemy_Type2s", -1f, 1f);
                    break;
            }
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

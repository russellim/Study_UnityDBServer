using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Type2s : MonoBehaviour
{
    public List<Enemy_Type2> AllType2s = new List<Enemy_Type2>();
    public List<Enemy_Type2> LiveType2s = new List<Enemy_Type2>();
    //bool AllDie = false;
    public float Speed;
    public float RunningFire = 1.5f;

    int Direction = 1;

    private void OnEnable()
    {
        LiveType2s = AllType2s;
        foreach(Enemy_Type2 e in AllType2s)
        {
            e.gameObject.SetActive(true);
        }
        StartCoroutine(Move());
        StartCoroutine(ChooseAttackChild());
    }

    IEnumerator Move()
    {
        while(true)
        {
            transform.Translate(Vector3.down * Speed * Time.deltaTime);
            if(transform.position.x >= 1f) Direction = -1;
            else if(transform.position.x <= -1f) Direction = 1;
            transform.Translate(Direction * Vector2.right * Speed * Time.deltaTime);

            if(transform.position.y <= -7f)
            {
                ObjectPool.Instance.PushToPool(gameObject.name, gameObject);
            }
            yield return null;
        }
    }

    IEnumerator ChooseAttackChild()
    {
        yield return new WaitForSeconds(0.3f);
        while (LiveType2s.Count != 0)
        {
            LiveType2s[Random.Range(0, LiveType2s.Count)].ImAttack = true;
            yield return new WaitForSeconds(RunningFire);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleDisable : MonoBehaviour
{
    ParticleSystem ps;

    private void Start()
    {
        ps = GetComponent<ParticleSystem>();
    }

    private void OnEnable()
    {
        StartCoroutine(Disable());
    }

    IEnumerator Disable()
    {
        yield return new WaitForSeconds(1f);
        ObjectPool.Instance.PushToPool(gameObject.name, gameObject);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PooledObject
{
    public string ObjectName = null;
    [SerializeField]
    GameObject Prefab = null;
    [SerializeField]
    int PoolCount = 0;
    [SerializeField]
    List<GameObject> PoolList = new List<GameObject>();

    public void Initialize(Transform Parent = null)
    {
        for (int i = 0; i < PoolCount; ++i)
        {
            PoolList.Add(CreateObject(Parent));
        }
    }

    GameObject CreateObject(Transform Parent = null)
    {
        GameObject OB = Object.Instantiate(Prefab);
        OB.name = ObjectName;
        OB.transform.SetParent(Parent);
        OB.SetActive(false);

        return OB;
    }

    public void PushToPool(GameObject OB, Transform Parent = null)
    {
        OB.transform.SetParent(Parent);
        OB.SetActive(false);
        PoolList.Add(OB);
    }

    public GameObject PopFromPool(Transform Parent = null)
    {
        if (PoolList.Count == 0)
            PoolList.Add(CreateObject(Parent));

        GameObject OB = PoolList[0];
        PoolList.RemoveAt(0);

        return OB;
    }
}
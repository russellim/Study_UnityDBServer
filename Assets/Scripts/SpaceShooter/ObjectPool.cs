using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : Singleton<ObjectPool>
{
    [SerializeField]
    List<PooledObject> Pool = new List<PooledObject>();

    private void Awake()
    {
        for (int i = 0; i < Pool.Count; ++i)
        {
            Pool[i].Initialize(transform);
        }
    }

    PooledObject GetPooledItem(string OBName)
    {
        for (int i = 0; i < Pool.Count; i++)
        {
            if (Pool[i].ObjectName.Equals(OBName))
            {
                return Pool[i];
            }
        }

        return null;
    }

    public bool PushToPool(string OBName, GameObject OB, Transform Parent = null)
    {
        PooledObject PooledItem = GetPooledItem(OBName);
        if (PooledItem == null)
        {
            print("pool 리스트에 오브젝트가 없습니다. 확인 부탁쓰");
            return false;
        }

        PooledItem.PushToPool(OB, Parent == null ? transform : Parent);

        return true;
    }

    public GameObject PopFromPool(string OBName, Transform Parent = null)
    {
        PooledObject PooledItem = GetPooledItem(OBName);
        if (PooledItem == null)
        {
            print("pool 리스트에 오브젝트가 없습니다. 확인 부탁쓰");
            return null;
        }

        return PooledItem.PopFromPool(Parent == null ? transform : Parent);
    }
}

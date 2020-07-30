using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    protected static T instance = null;

    public static T Instance
    {
        get
        {
            instance = FindObjectOfType(typeof(T)) as T;

            if(instance == null)
            {
                instance = new GameObject("@" + typeof(T).ToString(), typeof(T)).AddComponent<T>();

                // 이거는 각자 알아서들.
                //DontDestroyOnLoad(instance);
            }
            return instance;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour
{
    public static Main Instance;

    public Web Web;
    public UserInfo UserInfo;

    private void Start()
    {
        Instance = this;
        if (!Web) Web = GetComponent<Web>();
        if (!UserInfo) UserInfo = GetComponent<UserInfo>();
    }
}

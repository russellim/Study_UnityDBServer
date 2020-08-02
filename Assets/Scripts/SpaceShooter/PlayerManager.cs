using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : Singleton<PlayerManager>
{
    public int FullHP = 5;
    public int CurrentHP = 3;
    public int PowerUp = 1;
    public float MultiBulletSpeed = 1f;
    public float MultiRunningFire = 1f;

    public bool IsDie = false;
    
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class TestPlayer
{
    public string Username;
    public string Password;

    public TestPlayer() {  }

    public TestPlayer(string name,  string pass)
    {
        Username = name;
        Password = pass;
    }
}

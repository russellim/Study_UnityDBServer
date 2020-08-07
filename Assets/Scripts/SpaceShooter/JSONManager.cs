using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleJSON;

public class JSONManager : Singleton<JSONManager>
{
    public List<PlayerLevel> PlayerLevels = new List<PlayerLevel>();

    private void Awake()
    {
        ParsePlayerLevelJson("PlayerLevel");
    }

    void ParsePlayerLevelJson(string FilePath)
    {
        TextAsset textAsset = Resources.Load(FilePath) as TextAsset;
        JSONNode root = JSON.Parse(textAsset.text);
        JSONNode item = root["PlayerLevel"];

        for(int i=0; i<item.Count; ++i)
        {
            JSONNode itemdata = item[i];
            PlayerLevels.Add(new PlayerLevel(itemdata["Level"].AsInt, itemdata["Exp"].AsInt, itemdata["EnemySpeed"].AsFloat));
        }
    }
}

[System.Serializable]
public class PlayerLevel
{
    public int Level;
    public int Exp;
    public float EnemySpeed;

    public PlayerLevel(int level, int exp, float enemyspeed)
    {
        Level = level;
        Exp = exp;
        EnemySpeed = enemyspeed;
    }
}

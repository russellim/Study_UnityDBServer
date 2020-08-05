using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager>
{
    public Text PlayerScore;
    public Text HighScore;

    public void UpdatePlayerScoreUI(int Score)
    {
        PlayerScore.text = Score.ToString("000000");
    }
    public void UpdateHighScoreUI(int Score)
    {
        HighScore.text = Score.ToString("000000");
    }

}

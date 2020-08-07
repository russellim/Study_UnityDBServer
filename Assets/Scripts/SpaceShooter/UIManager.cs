using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager>
{
    public Text PlayerScore;
    public Text HighScore;

    public Text LevelText;
    public Image ExpProgress;

    public GameObject GameOverUI;

    public void UpdatePlayerScoreUI(int Score)
    {
        PlayerScore.text = Score.ToString("000000");
    }
    public void UpdateHighScoreUI(int Score)
    {
        HighScore.text = Score.ToString("000000");
    }


    public void UpdateLevelText(int Level)
    {
        if(Level >= 11)
        {
            LevelText.text = "MAX";
        }
        else
        {
            LevelText.text = Level.ToString();
        }
    }
    public void UpdateExpProgress(int Exp, int NeedExp)
    {
        ExpProgress.fillAmount = (float)Exp / (float)NeedExp;
    }


    public void DisplayGameOverUI()
    {
        GameOverUI.SetActive(true);
    }
    public void OnClickReplayButton()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Loading");
    }

}

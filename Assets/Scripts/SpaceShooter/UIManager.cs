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

    public GameObject PauseButton;

    public GameObject GameOverUI;
    public GameObject RankingWin;
    public GameObject RankTableWin;
    public GameObject GameOverWin;
    public GameObject PauseUI;
    public GameObject LoadingUI;

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


    public void OnClickRankingCancelButton()
    {
        RankingWin.SetActive(false);
        GameOverWin.SetActive(true);
    }
    public void OnClickGameOverBackButton()
    {
        GameOverWin.SetActive(false);
        RankingWin.SetActive(true);
    }
    public void OnClickPauseButton()
    {
        Time.timeScale = 0f;
        PauseUI.SetActive(true);
    }
    public void OnClickRetryButton()
    {
        Time.timeScale = 1f;
        Player.Instance.col.enabled = false;
        GameOverUI.SetActive(false);
        PauseUI.SetActive(false);
        LoadingUI.SetActive(true);
    }
    public void OnClickResumeButton()
    {
        Time.timeScale = 1f;
        PauseUI.SetActive(false);
    }
    public void OnClickMainButton()
    {
        Time.timeScale = 1f;
        UnityEngine.SceneManagement.SceneManager.LoadScene("Main");
    }

}

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

    [SerializeField]
    GameObject[] HPUI = null;

    public GameObject PauseButton;

    public GameObject GameOverUI;
    public GameObject RankingWin;
    public GameObject RankTableWin;
    public GameObject GameOverWin;
    public GameObject PauseWin;
    public GameObject LoadingUI;
    public GameObject SettingWin;

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

    public void UpdateHPUI(int CurrentHP, bool IsHeal)
    {
        if(IsHeal)
        {
            HPUI[CurrentHP - 1].SetActive(true);
        }
        else
        {
            HPUI[CurrentHP].SetActive(false);
        }
    }


    public void DisplayGameOverUI()
    {
        GameOverUI.SetActive(true);
    }


    public void OnClickRankingCancelButton()
    {
        SoundContoller.Instance.ButtonSound.Play();
        RankingWin.SetActive(false);
        GameOverWin.SetActive(true);
    }
    public void OnClickGameOverBackButton()
    {
        SoundContoller.Instance.ButtonSound.Play();
        GameOverWin.SetActive(false);
        RankingWin.SetActive(true);
    }
    public void OnClickPauseButton()
    {
        SoundContoller.Instance.PauseSound.Play();
        Time.timeScale = 0f;
        PauseWin.SetActive(true);
    }
    public void OnClickRetryButton()
    {
        SoundContoller.Instance.PlayStartSound.Play();
        Time.timeScale = 1f;
        Player.Instance.col.enabled = false;
        GameOverUI.SetActive(false);
        PauseWin.SetActive(false);
        LoadingUI.SetActive(true);
    }
    public void OnClickResumeButton()
    {
        SoundContoller.Instance.ButtonSound.Play();
        Time.timeScale = 1f;
        PauseWin.SetActive(false);
    }
    public void OnClickSettingButton()
    {
        SoundContoller.Instance.ButtonSound.Play();
        PauseWin.SetActive(false);
        SettingWin.SetActive(true);
    }
    public void OnCloseSettingWin()
    {
        SoundContoller.Instance.ButtonSound.Play();
        PauseWin.SetActive(true);
        SettingWin.SetActive(false);
    }
    public void OnClickMainButton()
    {
        SoundContoller.Instance.ButtonSound.Play();
        Time.timeScale = 1f;
        UnityEngine.SceneManagement.SceneManager.LoadScene("Main");
    }
}

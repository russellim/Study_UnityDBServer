using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadingManager : MonoBehaviour
{
    [SerializeField]
    Image LoadingBarProgress;
    [SerializeField]
    Text LoadingInfoText;

    private void Start()
    {
        LoadingBarProgress.fillAmount = 0;
        StartCoroutine(LoadAsyncScene("DBTutorial"));
    }

    public static void LoadScene(string SceneName)
    {
        SceneManager.LoadScene("Loading");
    }

    IEnumerator LoadAsyncScene(string NextSceneName)
    {
        yield return null;
        AsyncOperation asyncScene = SceneManager.LoadSceneAsync(NextSceneName);
        asyncScene.allowSceneActivation = false;
        float time = 0f;

        while(!asyncScene.isDone)
        {
            yield return null;
            time += Time.deltaTime;
            if(asyncScene.progress >= 0.9f)
            {
                LoadingBarProgress.fillAmount = Mathf.Lerp(LoadingBarProgress.fillAmount, 1, time);
                if(LoadingBarProgress.fillAmount == 1.0f)
                {
                    LoadingInfoText.text = "Complete!";
                    yield return new WaitForSeconds(1f);
                    asyncScene.allowSceneActivation = true;
                }
            }
            else
            {
                LoadingBarProgress.fillAmount = Mathf.Lerp(LoadingBarProgress.fillAmount, asyncScene.progress, time);
                if(LoadingBarProgress.fillAmount >= asyncScene.progress)
                {
                    time = 0f;
                }
            }
        }
    }

}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainManager : MonoBehaviour
{
    public GameObject MainUI;
    public GameObject LoadingUI;

    public void OnClickStartButton()
    {
        MainUI.SetActive(false);
        LoadingUI.SetActive(true);
    }

    public void OnClickExitButton()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}

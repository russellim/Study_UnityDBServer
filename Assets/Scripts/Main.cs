using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Main : MonoBehaviour
{
    public static Main Instance;

    public Web Web;
    public UserInfo UserInfo;

    public GameObject UserProfile;

    public Animator ErrorMessageAnim;
    public Text ErrorMessageText; 

    private void Start()
    {
        Instance = this;
        if (!Web) Web = GetComponent<Web>();
        if (!UserInfo) UserInfo = GetComponent<UserInfo>();
    }

    public void DisplayErrorMessage(string ErrorText)
    {
        ErrorMessageText.text = ErrorText;
        ErrorMessageAnim.Play("ErrorTextAnim");
    }
}

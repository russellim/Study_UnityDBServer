using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Main : MonoBehaviour
{
    public static Main Instance;

    public Web Web;
    public UserInfo UserInfo;
    public Login Login;

    public GameObject UserProfile;

    public Animator ErrorMessageAnim;
    public Text ErrorMessageText; 

    private void Start()
    {
        Instance = this;
        if (!Web) Web = GetComponent<Web>();
        if (!UserInfo) UserInfo = GetComponent<UserInfo>();

        TestDelegate.p2 += DisplayErrorMessage;
    }

    public void DisplayErrorMessage(string ErrorText)
    {
        ErrorMessageText.text = ErrorText;
        ErrorMessageAnim.Play("ErrorTextAnim");
    }

    public void OnClickLogoutButton()
    {
        UserInfo.SetCredentials(null, null, null, null);
        UserInfo.SetID(null);
        UserProfile.SetActive(false);
        Login.PasswordInput.text = "";
        Login.gameObject.SetActive(true);
    }
}

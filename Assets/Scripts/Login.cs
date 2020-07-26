using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using SimpleJSON;

public class Login : MonoBehaviour
{
    public InputField UsernameInput;
    public InputField PasswordInput;
    public Button LoginButton;

    public void OnClickLoginButton()
    {
        Action<string> _getPlayerInfoCallback = (playerInfo) =>
        {
            JSONArray tempArray = JSON.Parse(playerInfo) as JSONArray;
            Debug.Log(tempArray);
            JSONObject PlayerInfoJson = tempArray[0].AsObject;

            Main.Instance.UserInfo.SetCredentials(UsernameInput.text, PasswordInput.text, PlayerInfoJson["level"], PlayerInfoJson["coins"]);
            Main.Instance.UserInfo.SetID(PlayerInfoJson["id"]);

            Main.Instance.UserProfile.SetActive(true);
            gameObject.SetActive(false);

        };
        StartCoroutine(Main.Instance.Web.Login(UsernameInput.text, PasswordInput.text, _getPlayerInfoCallback));
    }

    private void Update()
    {
        if (UsernameInput.isFocused && Input.GetKeyDown(KeyCode.Tab))
        {
            PasswordInput.ActivateInputField();
        }
    }
}

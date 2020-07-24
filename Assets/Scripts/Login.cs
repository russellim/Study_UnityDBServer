using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Login : MonoBehaviour
{
    public InputField UsernameInput;
    public InputField PasswordInput;
    public Button LoginButton;

    public void OnClickLoginButton()
    {
        StartCoroutine(Main.Instance.Web.Login(UsernameInput.text, PasswordInput.text, gameObject));
    }

    private void Update()
    {
        if (UsernameInput.isFocused && Input.GetKeyDown(KeyCode.Tab))
        {
            PasswordInput.ActivateInputField();
        }
    }
}

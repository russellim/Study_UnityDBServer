using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RegisterUser : MonoBehaviour
{
    public InputField UsernameInput;
    public InputField PasswordInput;
    public InputField ConfirmPasswordInput;
    public Button SubmitButton;
    public Button ResetButton;
    public Button ExitButton;

    private void Start()
    {
        SubmitButton.onClick.AddListener(() =>
        {
            if(PasswordInput.text == ConfirmPasswordInput.text)
            {
                StartCoroutine(Main.Instance.Web.RegisterUser(UsernameInput.text, PasswordInput.text));
                gameObject.SetActive(false);
                CloseWindow();
            }
            else
            {
                Debug.Log("Check Your Password.");
            }
        });

        ResetButton.onClick.AddListener(() =>
        {
            ResetWindow();
        });

        ExitButton.onClick.AddListener(() =>
        {
            CloseWindow();
        });
    }

    void ResetWindow()
    {
        UsernameInput.text = "";
        PasswordInput.text = "";
        ConfirmPasswordInput.text = "";
    }

    void CloseWindow()
    {
        ResetWindow();
        gameObject.SetActive(false);
    }
}

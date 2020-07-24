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
                StartCoroutine(Main.Instance.Web.RegisterUser(UsernameInput.text, PasswordInput.text, gameObject));
                //CloseWindow();
            }
            else
            {
                Main.Instance.DisplayErrorMessage("Passwords are different.");
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

    private void Update()
    {
        if (UsernameInput.isFocused && Input.GetKeyDown(KeyCode.Tab))
        {
            PasswordInput.ActivateInputField();
        }
        if (PasswordInput.isFocused && Input.GetKeyDown(KeyCode.Tab))
        {
            ConfirmPasswordInput.ActivateInputField();
        }
    }

    private void OnEnable()
    {
        ResetWindow();
    }

    void ResetWindow()
    {
        UsernameInput.text = "";
        PasswordInput.text = "";
        ConfirmPasswordInput.text = "";
    }

    void CloseWindow()
    {
        gameObject.SetActive(false);
    }
}

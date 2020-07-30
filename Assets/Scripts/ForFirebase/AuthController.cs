using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Firebase.Auth;

public class AuthController : MonoBehaviour
{
    public InputField EmailInput, PasswordInput;

    public void Login()
    {
        FirebaseAuth.DefaultInstance.SignInWithEmailAndPasswordAsync(EmailInput.text, PasswordInput.text).ContinueWith(task =>
        {
            if(task.IsCanceled)
            {
                Firebase.FirebaseException e = task.Exception.Flatten().InnerExceptions[0] as Firebase.FirebaseException;
                GetErrorMessage((AuthError)e.ErrorCode);
                return;
            }

            if(task.IsFaulted)
            {
                Firebase.FirebaseException e = task.Exception.Flatten().InnerExceptions[0] as Firebase.FirebaseException;
                GetErrorMessage((AuthError)e.ErrorCode);
                return;
            }

            if(task.IsCompleted)
            {
                print("User is LOGGED IN!");
            }
        });
    }

    public void Login_Anonymous()
    {
        FirebaseAuth.DefaultInstance.SignInAnonymouslyAsync().ContinueWith(task =>
        {
            if (task.IsCanceled)
            {
                Firebase.FirebaseException e = task.Exception.Flatten().InnerExceptions[0] as Firebase.FirebaseException;
                GetErrorMessage((AuthError)e.ErrorCode);
                return;
            }

            if (task.IsFaulted)
            {
                Firebase.FirebaseException e = task.Exception.Flatten().InnerExceptions[0] as Firebase.FirebaseException;
                GetErrorMessage((AuthError)e.ErrorCode);
                return;
            }

            if (task.IsCompleted)
            {
                print("Anonymous is LOGGED IN!");
            }
        });
    }

    public void RegisterUser()
    {
        if(EmailInput.text.Equals("") && PasswordInput.text.Equals(""))
        {
            print("Please enter an email and password to register");
            return;
        }

        FirebaseAuth.DefaultInstance.CreateUserWithEmailAndPasswordAsync(EmailInput.text, PasswordInput.text).ContinueWith(task =>
        {
            if(task.IsCanceled)
            {
                Firebase.FirebaseException e = task.Exception.Flatten().InnerExceptions[0] as Firebase.FirebaseException;
                GetErrorMessage((AuthError)e.ErrorCode);
                return;
            }

            if(task.IsFaulted)
            {
                Firebase.FirebaseException e = task.Exception.Flatten().InnerExceptions[0] as Firebase.FirebaseException;
                GetErrorMessage((AuthError)e.ErrorCode);
                return;
            }

            if(task.IsCompleted)
            {
                print("Registration COMPLETE!");
            }

        });
    }

    public void Logout()
    {
        if(FirebaseAuth.DefaultInstance.CurrentUser != null)
        {
            FirebaseAuth.DefaultInstance.SignOut();
            print("See Ya!");
        }
        else
        {
            print("You didn't log in.");
        }
    }
    
    void GetErrorMessage(AuthError errorCode)
    {
        string msg = "";
        msg = errorCode.ToString();

        //switch(errorCode)
        //{
        //    case AuthError.AccountExistsWithDifferentCredentials:
        //        break;
        //    case AuthError.MissingPassword:
        //        break;
        //    case AuthError.WrongPassword:
        //        break;
        //    case AuthError.InvalidEmail:
        //        break;
        //}

        print(msg);
    }
}

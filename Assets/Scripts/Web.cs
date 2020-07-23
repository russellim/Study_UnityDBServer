using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Web : MonoBehaviour
{
    void Start()
    {
        // A correct website page.
        //StartCoroutine(GetRequest("http://localhost/UnityBackendTutorial/GetDate.php"));
        //StartCoroutine(GetRequest("http://localhost/unityBackendTutorial/GetUsers.php"));

        //StartCoroutine(Login("testuser1", "123456"));

        //StartCoroutine(RegisterUser("testuser3", "123456"));
    }

    public void ShowUserItems()
    {
        StartCoroutine(GetItemsIDs(Main.Instance.UserInfo.UserID));
    }

    IEnumerator GetRequest(string uri)
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(uri))
        {
            // Request and wait for the desired page.
            yield return webRequest.SendWebRequest();

            string[] pages = uri.Split('/');
            int page = pages.Length - 1;

            if (webRequest.isNetworkError)
            {
                Debug.Log(pages[page] + ": Error: " + webRequest.error);
            }
            else
            {
                Debug.Log(/*pages[page] + ":\nReceived: " +*/ webRequest.downloadHandler.text);
            }
        }
    }


    public IEnumerator Login(string username, string password)
    {
        WWWForm form = new WWWForm();
        form.AddField("loginUser", username);
        form.AddField("loginPass", password);

        using (UnityWebRequest www = UnityWebRequest.Post("http://localhost/unityBackendTutorial/Login.php", form))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
                Main.Instance.DisplayErrorMessage("Server connection failed.");
            }
            else
            {
                string resultText = www.downloadHandler.text;
                Debug.Log(resultText);

                if(resultText == "Wrong Credentials." || resultText == "Username does not exists.")
                {
                    Main.Instance.DisplayErrorMessage("Check your username and password again.");
                }
                else
                {
                    Main.Instance.UserInfo.SetCredentials(username, password);
                    Main.Instance.UserInfo.SetID(resultText);
                }
            }
        }
    }

    public IEnumerator RegisterUser(string username, string password)
    {
        WWWForm form = new WWWForm();
        form.AddField("loginUser", username);
        form.AddField("loginPass", password);

        using (UnityWebRequest www = UnityWebRequest.Post("http://localhost/unityBackendTutorial/RegisterUser.php", form))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {
                Debug.Log(www.downloadHandler.text);
            }
        }
    }

    IEnumerator GetItemsIDs(string userID)
    {
        WWWForm form = new WWWForm();
        form.AddField("userid", userID);

        using (UnityWebRequest www = UnityWebRequest.Post("http://localhost/unityBackendTutorial/GetItemsIDs.php", form))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {
                Debug.Log(www.downloadHandler.text);

                string jsonArray = www.downloadHandler.text;

                //Call callback function to pass results
            }
        }

    }
}

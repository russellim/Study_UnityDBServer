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


    public IEnumerator Login(string username, string password, System.Action<string> callback)
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

                if(resultText.Contains("Wrong Credentials.") || resultText.Contains("Username does not exists."))
                {
                    Main.Instance.DisplayErrorMessage("Check your username and password again.");
                }
                else
                {
                    // 로그인 성공.
                    callback(resultText);
                }

            }
        }
    }

    public IEnumerator RegisterUser(string username, string password, GameObject go = null)
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
                Main.Instance.DisplayErrorMessage("Server connection failed.");
            }
            else
            {
                string resultText = www.downloadHandler.text;
                Debug.Log(resultText);

                if (resultText.Contains("Username is already taken."))
                {
                    Main.Instance.DisplayErrorMessage(resultText);
                }
                else
                {
                    go.SetActive(false);
                }
            }
        }
    }

    public IEnumerator GetItemsIDs(string userID, System.Action<string> callback)
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
                string jsonArray = www.downloadHandler.text;
                Debug.Log(jsonArray);

                //Call callback function to pass results
                //와우 코루틴에서 반환값이 생기넹.
                callback(jsonArray);
            }
        }
    }

    public IEnumerator GetItem(string itemID, System.Action<string> callback)
    {
        WWWForm form = new WWWForm();
        form.AddField("itemid", itemID);

        using (UnityWebRequest www = UnityWebRequest.Post("http://localhost/unityBackendTutorial/GetItem.php", form))
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
                //와우 코루틴에서 반환값이 생기넹.
                callback(jsonArray);
            }
        }
    }

    public IEnumerator GetItemIcon(string itemID, System.Action<byte[]> callback)
    {
        WWWForm form = new WWWForm();
        form.AddField("itemid", itemID);

        using (UnityWebRequest www = UnityWebRequest.Post("http://localhost/unityBackendTutorial/GetItemIcon.php", form))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {
                Debug.Log("DOWNLOADING ICON: " + itemID);
                // byte는 .data!
                byte[] bytes = www.downloadHandler.data;
                callback(bytes);
            }
        }
    }

    public IEnumerator SellItem(string ID, string itemID, string userID)
    {
        WWWForm form = new WWWForm();
        form.AddField("id", ID);
        form.AddField("itemid", itemID);
        form.AddField("userid", userID);

        using (UnityWebRequest www = UnityWebRequest.Post("http://localhost/unityBackendTutorial/SellItem.php", form))
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
}


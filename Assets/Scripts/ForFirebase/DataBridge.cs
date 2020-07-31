using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Firebase;
using Firebase.Database;
using Firebase.Unity.Editor;
using Firebase.Auth;

public class DataBridge : MonoBehaviour
{
    public InputField UserNameInput, PasswordInput;

    private Player data;

    private string DATA_URL = "https://fir-and-unity-tutorial-fa8d9.firebaseio.com/";

    private DatabaseReference databaseReference;

    private void Start()
    {
        FirebaseApp.DefaultInstance.SetEditorDatabaseUrl(DATA_URL);
        databaseReference = FirebaseDatabase.DefaultInstance.RootReference;
    }

    public void SaveData()
    {
        if(UserNameInput.text.Equals("") && PasswordInput.text.Equals(""))
        {
            print("NO DATA");
            return;
        }

        data = new Player(UserNameInput.text, PasswordInput.text);
        string jsonData = JsonUtility.ToJson(data);


        databaseReference.Child("Users" + Random.Range(0, 10000)).SetRawJsonValueAsync(jsonData);
        //databaseReference.Child("Users").SetRawJsonValueAsync(jsonData);
    }

    public void LoadData()
    {
        FirebaseDatabase.DefaultInstance.GetReferenceFromUrl(DATA_URL).GetValueAsync().ContinueWith( task =>
        {
            if(task.IsFaulted)
            {

            }

            if(task.IsCanceled)
            {

            }

            if(task.IsCompleted)
            {
                DataSnapshot snapshot = task.Result;
                // 저장을 SetRawJsonValueAsync로 했으므로 GetRawJsonValue.
                string PlayerData = snapshot.GetRawJsonValue();
                //print("Data is " + PlayerData);

                Player m = JsonUtility.FromJson<Player>(PlayerData);
                foreach(var child in snapshot.Children)
                {
                    string t = child.GetRawJsonValue();
                    Player extractedData = JsonUtility.FromJson<Player>(t);
                    print("The Player's username is " + extractedData.Username);
                    print("The Player's password is " + extractedData.Password);
                }
            }
        });

    }
}

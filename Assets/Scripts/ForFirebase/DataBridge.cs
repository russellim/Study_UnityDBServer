using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Firebase;
using Firebase.Database;
using Firebase.Unity.Editor;
using Firebase.Auth;
using System;

[Serializable]
public class PlayerScoreDB
{
    public string Name;
    public int Level;
    public int Score;
    public float Time;
    public string Date;

    public PlayerScoreDB() { }

    public PlayerScoreDB(string name, int level, int score, float time)
    {
        Name = name;
        Level = level;
        Score = score;
        Time = time;
        Date = DateTime.Now.ToString("yyyy/MM/dd");
    }
}

public class DataBridge : MonoBehaviour
{
    public InputField PlayerNameInput;

    private PlayerScoreDB data;

    private string DATA_URL = "https://fir-and-unity-tutorial-fa8d9.firebaseio.com/";

    private DatabaseReference databaseReference;

    private void Start()
    {
        FirebaseApp.DefaultInstance.SetEditorDatabaseUrl(DATA_URL);
        databaseReference = FirebaseDatabase.DefaultInstance.RootReference;
    }

    private void OnEnable()
    {
        PlayerNameInput.text = PlayerPrefs.GetString("PlayerName", "");
    }

    public void SaveData()
    {
        if(PlayerNameInput.text.Equals(""))
        {
            // 이름을 입력해주세요!
            return;
        }

        PlayerPrefs.SetString("PlayerName", PlayerNameInput.text);

        data = new PlayerScoreDB(PlayerNameInput.text, Player.Instance.Level, GameManager.Instance.Score, GameManager.Instance.PlayTime);
        string jsonData = JsonUtility.ToJson(data);


        // Push()로 고유키값 만들어짐.
        databaseReference.Child("PlayerScore").Push().SetRawJsonValueAsync(jsonData);
        //databaseReference.Child("Users").SetRawJsonValueAsync(jsonData);
    }

    //public void LoadData()
    //{
    //    FirebaseDatabase.DefaultInstance.GetReferenceFromUrl(DATA_URL).GetValueAsync().ContinueWith( task =>
    //    {
    //        if(task.IsFaulted)
    //        {

    //        }

    //        if(task.IsCanceled)
    //        {

    //        }

    //        if(task.IsCompleted)
    //        {
    //            DataSnapshot snapshot = task.Result;
    //             저장을 SetRawJsonValueAsync로 했으므로 GetRawJsonValue.
    //            string PlayerData = snapshot.GetRawJsonValue();
    //            print("Data is " + PlayerData);

    //            PlayerScoreDB m = JsonUtility.FromJson<PlayerScoreDB>(PlayerData);
    //            foreach(var child in snapshot.Children)
    //            {
    //                string t = child.GetRawJsonValue();
    //                PlayerScoreDB extractedData = JsonUtility.FromJson<PlayerScoreDB>(t);
    //                print("The Player's username is " + extractedData.Username);
    //                print("The Player's password is " + extractedData.Password);
    //            }
    //        }
    //    });

    //}
}

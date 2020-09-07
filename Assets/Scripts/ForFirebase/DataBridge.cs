using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Firebase;
using Firebase.Database;
using Firebase.Unity.Editor;
using Firebase.Auth;
using System;
using UnityEngine.Networking;
using SimpleJSON;

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

    public GameObject RankWin;
    public RankTable rankTable;
    public GameObject LoadingImage;

    private PlayerScoreDB data;
    private DatabaseReference databaseReference;

    // phplocalhost server.
    string _path = "http://neneg.dothome.co.kr/UnityBackendTutorial/";

    List<PlayerScoreDB> temp1;
    List<PlayerScoreDB> temp2;

    private void Start()
    {
        FirebaseApp.DefaultInstance.SetEditorDatabaseUrl("https://fir-and-unity-tutorial-fa8d9.firebaseio.com/");
        databaseReference = FirebaseDatabase.DefaultInstance.GetReference("PlayerScore");
        temp1 = new List<PlayerScoreDB>();
        temp2 = new List<PlayerScoreDB>();
    }

    private void OnEnable()
    {
        if(PlayerNameInput) PlayerNameInput.text = PlayerPrefs.GetString("PlayerName", "");

        rankTable.OnChangeTextValue(0, 0, 0, "Name");
        rankTable.OnChangeTextValue(0, 0, 1, "Level");
        rankTable.OnChangeTextValue(0, 0, 2, "Score");
        rankTable.OnChangeTextValue(0, 0, 3, "Time");

        rankTable.OnChangeTextValue(1, 0, 0, "Name");
        rankTable.OnChangeTextValue(1, 0, 1, "Level");
        rankTable.OnChangeTextValue(1, 0, 2, "Time");

    }

    public void OnClickRank()
    {
        RankWin.SetActive(true);
        rankTable.FirstToggle.isOn = true;
        LoadScoreRankData();
        StartCoroutine(LoadTimeRankData());
        StartCoroutine(ShowScoreRankTable());
        StartCoroutine(ShowTimeRankTable());
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


        // 파이어베이스 저장.
        // Push()로 고유키값 만들어짐.
        databaseReference.Push().SetRawJsonValueAsync(jsonData);
        //databaseReference.Child("Users").SetRawJsonValueAsync(jsonData);

        // PHP서버 저장.
        StartCoroutine(AddPlayerScore_PHPServer());


        UIManager.Instance.OnClickRankingCancelButton();
        UIManager.Instance.GameOverBackButton.interactable = false;
        OnClickRank();

    }

    public IEnumerator AddPlayerScore_PHPServer()
    {
        WWWForm form = new WWWForm();
        form.AddField("Date", data.Date);
        form.AddField("Level", data.Level);
        form.AddField("Name", data.Name);
        form.AddField("Score", data.Score);
        form.AddField("Time", data.Time.ToString());

        using (UnityWebRequest www = UnityWebRequest.Post(_path + "AddPlayerScore.php", form))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {
                string resultText = www.downloadHandler.text;
                Debug.Log(resultText);
            }
        }
    }




    public void LoadScoreRankData()
    {
        temp1.Clear();
        databaseReference.OrderByChild("Score").LimitToLast(10).GetValueAsync().ContinueWith(task =>
       {
           if (task.IsCompleted)
           {
               DataSnapshot snapshot = task.Result;

               //저장을 SetRawJsonValueAsync로 했으므로 GetRawJsonValue.
               string PlayerData = snapshot.GetRawJsonValue();
               print("Data is " + PlayerData);

               PlayerScoreDB m = JsonUtility.FromJson<PlayerScoreDB>(PlayerData);

               foreach (var child in snapshot.Children)
               {
                   string t = child.GetRawJsonValue();
                   //PlayerScoreDB extractedData = JsonUtility.FromJson<PlayerScoreDB>(t);
                   temp1.Add(JsonUtility.FromJson<PlayerScoreDB>(t));
               }
               print("ok complete!");
               temp1.Reverse();
           }
       });
    }

    public IEnumerator LoadTimeRankData()
    {
        temp2.Clear();
        bool IsDone = false;

        JSONArray jsonArray = null;
        Action<string>_getPlayerScoreCallback = (PlayerScores) =>
        {
            IsDone = true;
            jsonArray = JSON.Parse(PlayerScores) as JSONArray;

        };
        StartCoroutine(GetPlayerScore_PHPServer(_getPlayerScoreCallback));

        yield return new WaitUntil(() => IsDone == true);

        for (int i = 0; i < jsonArray.Count; ++i)
        {
            temp2.Add(new PlayerScoreDB(jsonArray[i].AsObject["Name"],
                                        jsonArray[i].AsObject["Level"].AsInt,
                                        0,
                                        jsonArray[i].AsObject["Time"]));
        }
        print("ok complete! 2");
    }

    public IEnumerator GetPlayerScore_PHPServer(System.Action<string> callback)
    {
        WWWForm form = new WWWForm();

        using (UnityWebRequest www = UnityWebRequest.Post(_path + "GetPlayerScore.php", form))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {
                string jsonArray = www.downloadHandler.text;

                //Call callback function to pass results
                //와우 코루틴에서 반환값이 생기넹.
                callback(jsonArray);
            }
        }
    }


    IEnumerator ShowScoreRankTable()
    {
        while(temp1.Count != 10)
        {
            yield return new WaitForSeconds(0.5f);
        }
        for(int i=0; i< 10; ++i)
        {
            rankTable.OnChangeTextValue(0, i + 1, 0, temp1[i].Name);
            rankTable.OnChangeTextValue(0, i + 1, 1, temp1[i].Level.ToString());
            rankTable.OnChangeTextValue(0, i + 1, 2, temp1[i].Score.ToString());
            rankTable.OnChangeTextValue(0, i + 1, 3, temp1[i].Time.ToString());
        }
        LoadingImage.SetActive(false);
    }
    IEnumerator ShowTimeRankTable()
    {
        while (temp2.Count != 10)
        {
            yield return new WaitForSeconds(0.5f);
        }
        for (int i = 0; i < 10; ++i)
        {
            rankTable.OnChangeTextValue(1, i + 1, 0, temp2[i].Name);
            rankTable.OnChangeTextValue(1, i + 1, 1, temp2[i].Level.ToString());
            rankTable.OnChangeTextValue(1, i + 1, 2, temp2[i].Time.ToString());
        }
        LoadingImage.SetActive(false);
    }

    public void OnClickCloseRank()
    {
        RankWin.SetActive(false);
    }
}

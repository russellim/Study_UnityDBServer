using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UI.TableUI;
using System.Collections;
using System.Collections.Generic;
using Firebase;
using Firebase.Database;
using Firebase.Unity.Editor;
using Firebase.Auth;
using System;

public class TableUIExample : MonoBehaviour
{
    public TableUI table;
    public Text rows, cols, text;

    List<string> li;
    List<PlayerScoreDB> temp;

    private string DATA_URL = "https://fir-and-unity-tutorial-fa8d9.firebaseio.com/";
    private DatabaseReference databaseReference;

    private void Start()
    {
        FirebaseApp.DefaultInstance.SetEditorDatabaseUrl(DATA_URL);
        databaseReference = FirebaseDatabase.DefaultInstance.GetReference("PlayerScore");

        li = new List<string>();
        li.Add("1");
        li.Add("22");
        li.Add("333");
        li.Add("4444");
        li.Add("55555");
        LoadScoreRankData();
        StartCoroutine(test());


    }
    IEnumerator test()
    {
        yield return new WaitForSeconds(3f);
        print(temp.Count);
        OnChangeTextValue(1, 0, temp[0].Name);
        OnChangeTextValue(1, 1, temp[1].Name);
        for(int i=0; i<temp.Count; ++i)
        {
            OnChangeTextValue(1, i, temp[i].Name);
        }
    }

    public void LoadScoreRankData()
    {
        temp = new List<PlayerScoreDB>();
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
                    temp.Add(JsonUtility.FromJson<PlayerScoreDB>(t));
                }
                print("ok complete!");

                temp.Reverse();
            }
        });
    }

    public void OnChangeTextValue(int r = -1, int c = -1, string value = "0")
    {
        if (r == -1)
        {
            r = System.Int32.Parse(rows.text);
            c = System.Int32.Parse(cols.text);
            value = text.text;
        }

        if (r < TableUI.MIN_ROWS - 1 || r >= table.Rows)
        {
            Debug.Log("The row number is not in range");
            return;
        }

        if (c < TableUI.MIN_COL - 1 || c >= table.Columns)
        {
            Debug.Log("The column number is not in range");
            return;
        }

        table.GetCell(r, c).text = value;
    }

    public void OnAddNewRowClick()
    {
        table.Rows++;
    }

    public void OnAddNewColumnClick()
    {
        table.Columns++;
    }

    public void OnRemoveLastColumn()
    {
        table.Columns--;
    }

    public void OnRemoveLastRow()
    {
        table.Rows--;
    }


}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using SimpleJSON;

public class Items : MonoBehaviour
{
    Action<string> _createItemsCallback;
    Action<string> _getItemInfoCallback;
    public GameObject ItemUI;

    private void OnEnable()
    {
        if(transform.childCount != 0)
        {
            for (int i = 0; i < transform.childCount; i++) 
            { 
                Destroy(transform.GetChild(i).gameObject); 
            }
        }
        // Define callback.
        _createItemsCallback = (jsonArrayString) => 
        {
            StartCoroutine(CreateItemsRoutine(jsonArrayString));
        };

        CreateItems();
    }

    public void CreateItems()
    {
        string userId = Main.Instance.UserInfo.UserID;
        StartCoroutine(Main.Instance.Web.GetItemsIDs(userId, _createItemsCallback));
    }

    IEnumerator CreateItemsRoutine(string jsonArrayString)
    {
        if(jsonArrayString == "0")
        {
            yield break;
        }

        // Parsing json array string as an array.
        JSONArray jsonArray = JSON.Parse(jsonArrayString) as JSONArray;
        
        for(int i=0; i<jsonArray.Count; ++i)
        {
            // Create local variables.
            bool IsDone = false;    // 다운로드 끝?
            string itemId = jsonArray[i].AsObject["itemid"];

            JSONObject ItemInfoJson = new JSONObject();
            // Create a callback to get the information from Web.cs.
            _getItemInfoCallback = (itemInfo) =>
            {
                IsDone = true;
                JSONArray tempArray = JSON.Parse(itemInfo) as JSONArray;
                ItemInfoJson = tempArray[0].AsObject;
            };

            StartCoroutine(Main.Instance.Web.GetItem(itemId, _getItemInfoCallback));

            // Wait until the callback is called from WEB (info finished downloading).
            yield return new WaitUntil(() => IsDone == true);

            // Instantiate GameObject.
            GameObject ItemOB = Instantiate(ItemUI);
            ItemOB.transform.SetParent(transform);

            // Fill Information.
            ItemOB.transform.Find("Name").GetComponent<Text>().text = ItemInfoJson["name"];
            ItemOB.transform.Find("Price").GetComponent<Text>().text = ItemInfoJson["price"];
            ItemOB.transform.Find("Description").GetComponent<Text>().text = ItemInfoJson["description"];

            // continue to the next item.
        }
    }
}

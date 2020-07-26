using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using SimpleJSON;

public class ItemManager : MonoBehaviour
{
    Action<string> _createItemsCallback;
    Action<string> _getItemInfoCallback;
    Action<byte[]> _getItemIconCallback;
    public GameObject ItemUI;
    public Transform parent;

    private void OnEnable()
    {
        if(parent.childCount != 0)
        {
            for (int i = 0; i < parent.childCount; i++) 
            { 
                Destroy(parent.GetChild(i).gameObject); 
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
            string id = jsonArray[i].AsObject["id"];

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
            Item item = ItemOB.AddComponent<Item>();
            item.ID = id;
            item.ItemID = itemId;
            ItemOB.transform.SetParent(parent);

            // Fill Information.
            ItemOB.transform.Find("Name").GetComponent<Text>().text = ItemInfoJson["name"];
            ItemOB.transform.Find("Price").GetComponent<Text>().text = ItemInfoJson["price"];
            ItemOB.transform.Find("Description").GetComponent<Text>().text = ItemInfoJson["description"];

            // 1. Get bytes instead of sprite.
            // 2. Try to get image.
            // 3. Download image only if we couldn't get image.
            // 4. Save image in our device of we downloaded it.
            // 5. Convert bytes into sprite here.

            int imgVer = ItemInfoJson["imgver"].AsInt;

            byte[] bytes = ImageManager.Instance.LoadImage(itemId, imgVer);

            if(bytes.Length == 0)
            {
                // Create a callback to get the sprite from Web.cs.
                _getItemIconCallback = (downloadedBytes) =>
                {
                    Sprite sprite = ImageManager.Instance.BytesToSprite(downloadedBytes);
                    ItemOB.transform.Find("Image").GetComponent<Image>().sprite = sprite;
                    ImageManager.Instance.SaveImage(itemId, downloadedBytes, imgVer);
                    ImageManager.Instance.SaveVersionJson();
                };
                StartCoroutine(Main.Instance.Web.GetItemIcon(itemId, _getItemIconCallback));
            }
            // Load from device.
            else
            {
                Sprite sprite = ImageManager.Instance.BytesToSprite(bytes);
                ItemOB.transform.Find("Image").GetComponent<Image>().sprite = sprite;
            }




            // Set Sell Button.
            // 각 버튼에 대해 리스너 넣어줌.
            // id, itemID를 이미 구해놔서 파라미터 적용이 쉽다 이말이야.
            ItemOB.transform.Find("SellButton").GetComponent<Button>().onClick.AddListener(() =>
            {
                StartCoroutine(Main.Instance.Web.SellItem(id, itemId, Main.Instance.UserInfo.UserID));
            });

            // continue to the next item.
        }
    }
}

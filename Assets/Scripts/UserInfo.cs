using UnityEngine;
using UnityEngine.UI;

public class UserInfo : MonoBehaviour
{
    public string UserID { get; private set; }
    string UserName;
    string UserPassword;
    string Level;
    string Coins;

    public Text UserNameText;
    public Text UserLevelText;
    public Text UserCoinsText;

    public void SetCredentials(string username, string userpassword, string userlevel, string usercoins)
    {
        UserName = UserNameText.text = username;
        UserPassword = userpassword;
        UserLevelText.text = "Level " + userlevel;
        UserCoinsText.text = usercoins + " Gold";
    }

    public void SetID(string id)
    {
        UserID = id;
    }
}

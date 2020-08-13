using UnityEngine;
using UnityEngine.UI.TableUI;
using UnityEngine.UI;

public class RankTable : MonoBehaviour
{
    public TableUI[] Tables;
    public Toggle FirstToggle;

    public void OnChangeTextValue(int tableNum, int row, int col, string value)
    {
        if (row < TableUI.MIN_ROWS - 1 || row >= Tables[tableNum].Rows)
        {
            Debug.Log("The row number is not in range");
            return;
        }

        if (col < TableUI.MIN_COL - 1 || col >= Tables[tableNum].Columns)
        {
            Debug.Log("The column number is not in range");
            return;
        }

        Tables[tableNum].GetCell(row, col).text = value;
    }

    public void OnClickScoreRank()
    {
        Tables[0].gameObject.SetActive(true);
        Tables[1].gameObject.SetActive(false);
    }
    public void OnClickTimeRank()
    {
        Tables[0].gameObject.SetActive(false);
        Tables[1].gameObject.SetActive(true);
    }
}

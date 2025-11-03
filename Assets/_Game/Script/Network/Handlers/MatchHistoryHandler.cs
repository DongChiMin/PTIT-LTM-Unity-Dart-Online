using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatchHistoryHandler 
{
    public MatchHistoryHandler()
    {

    }

    
    public void Handle(SimpleJSON.JSONNode jsonData)
    {
        string status = jsonData["status"].Value;
        switch (status)
        {
            case "SUCCESS":
                
                UIManager.Instance.ShowOnly(UIPaneltype.matchHistory);
                break;

            case "FAIL":
                Debug.Log("status của msg = FAIL");
                break;

            default:
                Debug.Log("Lỗi status của dữ liệu:" + jsonData);
                break;
        }
    }
}

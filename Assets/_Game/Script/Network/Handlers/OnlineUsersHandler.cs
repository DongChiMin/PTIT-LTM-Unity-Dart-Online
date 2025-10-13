using SimpleJSON;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnlineUsersHandler
{
    public OnlineUsersHandler()
    {

    }

    public void Handle(SimpleJSON.JSONNode jsonData)
    {
        string status = jsonData["status"].Value;
        switch (status)
        {
            case "SUCCESS":
                JSONArray users = jsonData["data"]["users"].AsArray;

                for (int i = 0; i < users.Count; i++)
                {
                    string playerName = users[i]["playerName"];
                    string userStatus = users[i]["status"];

                    PlayerOnlineListController.Instance.SetOnlineUsersList(i.ToString(), playerName, userStatus);
                }
                break;
            case "FAIL":
                Debug.Log("Lấy danh sách người chơi online thất bại");
                break;
            default:
                Debug.Log("Lỗi status của dữ liệu:" + jsonData);
                break;
        }
    }
}

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
                PlayerOnlineListController.Instance.ResetOnlineUsersList();
                JSONArray users = jsonData["data"]["users"].AsArray;
                for (int i = 0; i < users.Count; i++)
                {
                    string playerName = users[i]["playerName"];
                    string userStatus = users[i]["status"];
                    string index = (i + 1).ToString();
                    PlayerOnlineListController.Instance.SetOnlineUsersList(users[i]["id"], index, playerName, userStatus);
                }
                break;
            case "FAIL":
                Debug.Log("Lấy danh sách người chơi online thất bại");
                PlayerOnlineListController.Instance.ResetOnlineUsersList();
                break;
            default:
                Debug.Log("Lỗi status của dữ liệu:" + jsonData);
                break;
        }
    }
}

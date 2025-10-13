using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InviteResponseHandler
{
    public InviteResponseHandler()
    {

    }

    public void Handle(SimpleJSON.JSONNode jsonData)
    {
        string status = jsonData["status"].Value;
        switch (status)
        {
            case "SUCCESS":
                UIManager.Instance.ShowOnly(UIPaneltype.inviteFailed);

                //Lấy data từ JSON
                string statusData = jsonData["data"]["status"].Value;
                string playerName = jsonData["data"]["fromUsername"].Value;

                //Set thông tin người chơi nào đã từ chối
                PlayerOnlineListController.Instance.SetInviteFailedText(playerName + " declined!");

                break;
            case "FAIL":
                UIManager.Instance.Show(UIPaneltype.loginFailed);
                break;
            default:
                Debug.Log("Lỗi status của dữ liệu:" + jsonData);
                break;
        }
    }
}

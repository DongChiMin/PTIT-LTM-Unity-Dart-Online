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
                

                //Lấy data từ JSON
                string statusData = jsonData["data"]["status"].Value;
                string playerName = jsonData["data"]["fromUsername"].Value;

                //Set thông tin người chơi nào đã từ chối
                if(statusData == "ACCEPT")
                {
                    UIManager.Instance.ShowOnly(UIPaneltype.playingDemo);
                    RoundController.Instance.SetPlayerP("P1");
                }
                else
                {
                    UIManager.Instance.Show(UIPaneltype.inviteFailed);
                    PlayerOnlineListController.Instance.SetInviteFailedText(playerName + " declined!");
                }
                    

                break;
            case "FAIL":
                
                break;
            default:
                Debug.Log("Lỗi status của dữ liệu:" + jsonData);
                break;
        }
    }
}

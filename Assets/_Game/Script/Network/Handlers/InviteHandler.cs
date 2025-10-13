
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InviteHandler
{
    public InviteHandler()
    {

    }

    public void Handle(SimpleJSON.JSONNode jsonData)
    {
        string status = jsonData["status"].Value;
        switch (status)
        {
            case "SUCCESS":
                //Chuyển sang UI chờ accept
                UIManager.Instance.ShowOnly(UIPaneltype.waitingAccept);

                //Set int của người mời chơi
                int fromPlayerId = jsonData["data"]["fromUserId"];
                string fromPlayerName = jsonData["data"]["fromUsername"];
                PlayerOnlineListController.Instance.SetInviteFromId(fromPlayerId);
                PlayerOnlineListController.Instance.SetWaitingAcceptText(fromPlayerName + " is inviting...");

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

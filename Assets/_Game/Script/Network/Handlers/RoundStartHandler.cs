using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoundStartHandler
{
    public RoundStartHandler()
    {

    }

    public void Handle(SimpleJSON.JSONNode jsonData)
    {
        string status = jsonData["status"].Value;
        switch (status)
        {
            

            case "SUCCESS":
                int matchId = jsonData["data"]["matchId"];
                int round = jsonData["data"]["round"];
                int firstTurnId = jsonData["data"]["firstTurnId"];
                string firstTurnName = jsonData["data"]["firstTurnName"];

                //Nếu người chơi không phải là người ném
                if(PlayerPrefs.GetInt("user_id") != firstTurnId)
                {
                    RoundController.Instance.SetFields(true, false);
                }
                else
                {
                    RoundController.Instance.SetFields(false, false);
                }
                RoundController.Instance.SetRoundText(matchId, firstTurnName, round);

                break;
            case "FAIL":
                
                break;
            default:
                Debug.Log("Lỗi status của dữ liệu:" + jsonData);
                break;
        }
    }
}

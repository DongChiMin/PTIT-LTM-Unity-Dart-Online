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
                string firstTurnName = jsonData["data"]["firstTurnName"].Value;
                if (PlayerPrefs.GetInt("user_id") != firstTurnId)
                {
                    RoundController.Instance.SetFields(false, false);
                }
                else
                {
                    RoundController.Instance.SetFields(true, false);
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

    public void HandleThrowForce(SimpleJSON.JSONNode jsonData)
    {
        string status = jsonData["status"].Value;
        switch (status)
        {
            case "SUCCESS":
                int matchId = jsonData["data"]["matchId"];
                int round = jsonData["data"]["round"];
                int firstTurnId = jsonData["data"]["firstTurnId"];
                string firstTurnName = jsonData["data"]["firstTurnName"].Value;

                if (PlayerPrefs.GetInt("user_id") != firstTurnId)
                {
                    RoundController.Instance.SetFields(false, false);
                }
                else
                {
                    RoundController.Instance.SetFields(true, false);
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

    public void HandleThrowScore(SimpleJSON.JSONNode jsonData)
    {
        string status = jsonData["status"].Value;
        switch (status)
        {


            case "SUCCESS":
                int matchId = jsonData["data"]["matchId"];
                int round = jsonData["data"]["round"];
                int firstTurnId = jsonData["data"]["firstTurnId"];
                string firstTurnName = jsonData["data"]["firstTurnName"].Value;

                Debug.Log("DATA" + jsonData);
                //Nếu người chơi không phải là người ném
                Debug.Log("ID CỦA NGƯỜI CHƠI HIỆN TẠI" + PlayerPrefs.GetInt("user_id"));
                Debug.Log("ID CỦA NGƯỜI ĐƯỢC NÉM TRƯỚC" + firstTurnId);
                if (PlayerPrefs.GetInt("user_id") != firstTurnId)
                {
                    RoundController.Instance.SetFields(false, false);
                }
                else
                {
                    RoundController.Instance.SetFields(true, false);
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

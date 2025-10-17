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

                //Nếu không phải là người ném trước
                if (PlayerPrefs.GetInt("user_id") != firstTurnId)
                {
                    PlayerController.Instance.SetIsThrower(false);
                    Dart dart = DartManager.Instance.ReloadDart();
                    PlayerController.Instance.SetDart(dart);
                    CameraManager.Instance.SetCameraFollow(dart.gameObject);
                    dart.SetIsThrower(false);

                    //Phiên bản playingDemo
                    RoundController.Instance.SetFields(false, false);
                }
                else
                {
                    PlayerController.Instance.SetIsThrower(true);
                    Dart dart = DartManager.Instance.ReloadDart();
                    PlayerController.Instance.SetDart(dart);
                    CameraManager.Instance.SetCameraFollow(dart.gameObject);
                    dart.SetIsThrower(true);

                    //Phiên bản playingDemo
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

    //{ "response":"response_throw_force","status":"SUCCESS","data":{ "matchId":1,"round":1,"playerId":2,"playerName":"hoang","force":0.0,"timeout":false} }
    public void HandleThrowForce(SimpleJSON.JSONNode jsonData)
    {
        string status = jsonData["status"].Value;
        switch (status)
        {
            case "SUCCESS":
                float force;
                //Nếu là người kia ném thì mới cập nhật lực
                if (PlayerPrefs.GetInt("user_id") != jsonData["data"]["playerId"])
                {
                    force = jsonData["data"]["force"];
                    PlayerController.Instance.ShootDart(force);
                }
                else
                {
                    force = -1f;
                }

                    RoundController.Instance.SetOpponentForceReceived(force);
                break;
            case "FAIL":

                break;
            default:
                Debug.Log("Lỗi status của dữ liệu:" + jsonData);
                break;
        }
    }

    //{ "response":"response_throw_score","status":"SUCCESS","data":{ "matchId":4,"round":1,"playerId":2,"playerName":"hoang","score":34,"totalScoreP1":34,"totalScoreP2":0,"timeout":false} }
    public void HandleRoundScore(SimpleJSON.JSONNode jsonData)
    {
        string status = jsonData["status"].Value;
        switch (status)
        {

            case "SUCCESS":
                int yourScore = jsonData["data"]["totalScoreP1"];
                int opponentScore = jsonData["data"]["totalScoreP2"];
                RoundController.Instance.SetScore(yourScore, opponentScore);
                break;
            case "FAIL":

                break;
            default:
                Debug.Log("Lỗi status của dữ liệu:" + jsonData);
                break;
        }
    }
}

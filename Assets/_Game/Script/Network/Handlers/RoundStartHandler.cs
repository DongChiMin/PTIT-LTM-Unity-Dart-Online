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

                long timeOut = jsonData["data"]["timeOut"];

                //Nếu không phải là người ném trước
                if (PlayerPrefs.GetInt("user_id") != firstTurnId)
                {
                    PlayerController.Instance.SetIsThrower(false);
                    Dart dart = DartManager.Instance.ReloadDart();
                    PlayerController.Instance.SetDart(dart);
                    CameraManager.Instance.SetCameraFollow(dart.gameObject);
                    dart.SetIsThrower(false);

                    RoundController.Instance.SetOpponentThrowingUI(true);

                    RoundController.Instance.SetRotateButton(true);

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

                    RoundController.Instance.SetOpponentThrowingUI(false);

                    RoundController.Instance.SetRotateButton(false);

                    //Phiên bản playingDemo
                    RoundController.Instance.SetFields(true, false);
                }
                RoundController.Instance.SetRoundText(matchId, firstTurnName, round, timeOut);
                RoundController.Instance.SetDartboardRotateSpeed(0f);
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
                    //PlayerController.Instance.ShootDart(force);
                }
                else
                {
                    force = -1f;
                }

                //Set lực ném trên màn hình
                    //RoundController.Instance.SetOpponentForceReceived(force);
                break;
            case "FAIL":

                break;
            default:
                Debug.Log("Lỗi status của dữ liệu:" + jsonData);
                break;
        }
    }

    //{ "response":"response_throw_swipe",
    //  "status":"SUCCESS","data":{
    //      "matchId":1,"round":1,
    //      "playerId":2,
    //      "playerName":"hoang",
    //      "swipeX":0.0,
    //      "swipeY":0.0,
    //      "timeout":false
    //   }
    //}
    public void HandleThrowSwipe(SimpleJSON.JSONNode jsonData)
    {
        string status = jsonData["status"].Value;
        switch (status)
        {
            case "SUCCESS":
                //Thực hiện hoạt ảnh và logic ném trên cả hai máy
                float swipeX = jsonData["data"]["swipeX"];
                float swipeY = jsonData["data"]["swipeY"];
                Vector2 swipe = new Vector2(swipeX, swipeY);

                PlayerController.Instance.ShootDart(swipe);

                //Set lực ném trên màn hình
                //RoundController.Instance.SetOpponentForceReceived(force);
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

    //{ "response":"response_rotate_dartboard","status":"SUCCESS","data":{ "rotateSpeed":10.2} }
    public void HandleRotateDartboard(SimpleJSON.JSONNode jsonData)
    {
        string status = jsonData["status"].Value;
        switch (status)
        {
            case "SUCCESS":
                float rotateSpeed = jsonData["data"]["rotateSpeed"];
                RoundController.Instance.SetDartboardRotateSpeed(rotateSpeed);
                break;
            case "FAIL":

                break;
            default:
                Debug.Log("Lỗi status của dữ liệu:" + jsonData);
                break;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatchEndHandler
{
    public MatchEndHandler()
    {

    }

    //{ "response":"response_match_end","status":"SUCCESS","data":{ "matchId":2,"idP1":2,"p1":"hoang","idP2":3,"p2":"nguyet","scoreP1":170,"scoreP2":50,"winner":"hoang"} }
    public void Handle(SimpleJSON.JSONNode jsonData)
    {
        string status = jsonData["status"].Value;
        switch (status)
        {
            case "SUCCESS":
                int scoreP1 = jsonData["data"]["scoreP1"];
                int scoreP2 = jsonData["data"]["scoreP2"];
                int idP1 = jsonData["data"]["idP1"];
                int idP2 = jsonData["data"]["idP2"];
                string nameP1 = jsonData["data"]["p1"].Value;
                string nameP2 = jsonData["data"]["p2"].Value;

                int yourScore;
                int opponentScore;
                int opponentId;
                string opponentName;
                if (PlayerPrefs.GetString("user_playerName").ToLower() == nameP1)
                {
                    yourScore = scoreP1;
                    opponentScore = scoreP2;
                    opponentId = idP2;
                    opponentName = nameP2;
                }
                else
                {
                    yourScore = scoreP2;
                    opponentScore = scoreP1;
                    opponentId = idP1;
                    opponentName = nameP1;
                }

                string reason = jsonData["data"]["reason"];

                //Nếu có người chơi thoát trận giữa chừng
                if (reason == "player_left")
                {
                    string leftPlayer = jsonData["data"]["leftPlayer"];
                    Debug.Log(PlayerPrefs.GetString("user_playerName").ToLower());
                    if(PlayerPrefs.GetString("user_playerName").ToLower() == leftPlayer)
                    {
                        //DEFEAT
                        MatchEndController.Instance.SetMatchEndContent(-1, opponentScore, -2, "DEFEAT");
                        MatchEndController.Instance.SetOpponentAttribute(opponentId, opponentName, reason);
                    }
                    else
                    {
                        //WIN
                        MatchEndController.Instance.SetMatchEndContent(yourScore, -1, 3, "VICTORY");
                        MatchEndController.Instance.SetOpponentAttribute(opponentId, opponentName, reason);
                    }
                }
                //Nếu không có trường hợp người chơi thoát trận giữa chừng
                else
                {
                    if (yourScore > opponentScore)
                    {
                        //WIN
                        MatchEndController.Instance.SetMatchEndContent(yourScore, opponentScore, 3, "VICTORY");
                        MatchEndController.Instance.SetOpponentAttribute(opponentId, opponentName, reason);
                    }
                    else if (yourScore < opponentScore)
                    {
                        //DEFEAT
                        MatchEndController.Instance.SetMatchEndContent(yourScore, opponentScore, -2, "DEFEAT");
                        MatchEndController.Instance.SetOpponentAttribute(opponentId, opponentName, reason);
                    }
                    else
                    {
                        //DRAW
                        MatchEndController.Instance.SetMatchEndContent(yourScore, opponentScore, +1, "DRAW");
                        MatchEndController.Instance.SetOpponentAttribute(opponentId, opponentName, reason);
                    }
                }
                UIManager.Instance.ShowOnly(UIPaneltype.matchEnd);
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

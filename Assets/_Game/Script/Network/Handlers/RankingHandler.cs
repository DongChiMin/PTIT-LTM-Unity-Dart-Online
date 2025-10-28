using SimpleJSON;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RankingHandler
{
    public RankingHandler()
    {

    }

    ////{"response":"response_ranking","status":"SUCCESS","data":{"ranking":[{"rank":1,"playerName":"Nguyet","totalScore":100},{"rank":2,"playerName":"Player1","totalScore":80},{"rank":3,"playerName":"Player2","totalScore":60}],"currentUserRank":1}}
    public void Handle(SimpleJSON.JSONNode jsonData)
    {
        string status = jsonData["status"].Value;
        switch (status)
        {
            case "SUCCESS":
                UIManager.Instance.ShowOnly(UIPaneltype.ranking);

                RankingController.Instance.ResetRankingList();

                JSONArray users = jsonData["data"]["ranking"].AsArray;
                for (int i = 0; i < users.Count; i++)
                {
                    int rank = users[i]["rank"];
                    string playerName= users[i]["playerName"];
                    int totalScore = users[i]["totalScore"];

                    RankingController.Instance.SetPlayerRanking(rank, playerName, totalScore);
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

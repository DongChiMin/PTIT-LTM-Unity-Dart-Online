using SimpleJSON;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatchHistoryHandler 
{
    public MatchHistoryHandler()
    {

    }


    public void Handle(SimpleJSON.JSONNode jsonData)
    {
        string status = jsonData["status"].Value;
        switch (status)
        {
            case "SUCCESS":
                // Dữ liệu thành công, xử lý lịch sử trận đấu
                JSONArray matches = jsonData["data"]["matches"].AsArray;

                MatchHistoryController.Instance.ResetMatchHistory();

                // Nếu có trận đấu, duyệt qua và hiển thị
                for (int i = 0; i < matches.Count; i++)
                {
                    // Lấy thông tin từ mỗi trận đấu
                    int matchId = matches[i]["matchId"];
                    string startTime = matches[i]["startTime"];
                    string endTime = matches[i]["endTime"];
                    string opponentName = matches[i]["opponentName"];
                    int yourScore = matches[i]["yourScore"];
                    int opponentScore = matches[i]["opponentScore"];
                    string result = matches[i]["result"];
                    string note = matches[i]["note"];

                    // Nếu cần hiển thị trên UI, bạn có thể gọi các hàm của UI controller ở đây
                    // Ví dụ: Update UI với thông tin lịch sử trận đấu
                    MatchHistoryController.Instance.AddMatchToHistory(matchId, startTime, endTime, opponentName, yourScore, opponentScore, result, note);
                }

                UIManager.Instance.ShowOnly(UIPaneltype.matchHistory);
                break;

            case "FAIL":
                // Nếu có lỗi, thông báo thất bại
                Debug.Log("Lấy lịch sử trận đấu thất bại");
                break;

            default:
                // Nếu status không phải là "SUCCESS" hoặc "FAIL", thông báo lỗi
                Debug.LogError("Lỗi status của dữ liệu: " + jsonData);
                break;
        }
    }

}

using SimpleJSON;
using UnityEngine;

public class RankingHandler
{
    public void Handle(JSONNode jsonData)
    {
        string status = jsonData["status"];
        switch (status)
        {
            case "SUCCESS":
                // 1. Reset danh sách cũ
                PlayerRankingController.Instance.ResetRankingList();

                // 2. Lấy dữ liệu từ JSON
                JSONArray rankingArray = jsonData["data"]["ranking"].AsArray;
                int currentUserRank = jsonData["data"]["currentUserRank"].AsInt;
                int currentUserId = jsonData["data"]["currentUserId"].AsInt;

                // 3. Gán ID người dùng hiện tại để Controller biết cần highlight ai
                PlayerRankingController.Instance.SetCurrentUserId(currentUserId);

                // 4. Lặp qua danh sách và gọi Controller để thêm từng hàng
                for (int i = 0; i < rankingArray.Count; i++)
                {
                    int playerId = rankingArray[i]["id"].AsInt;
                    string playerName = rankingArray[i]["playerName"];
                    int score = rankingArray[i]["score"].AsInt;

                    PlayerRankingController.Instance.AddRankingRow(i + 1, playerName, score, playerId);
                }

                // 5. Cập nhật text hiển thị hạng
                PlayerRankingController.Instance.SetCurrentUserRank(currentUserRank);

                // 6. Hiển thị panel
                UIManager.Instance.ShowOnly(UIPaneltype.playerRanking);
                break;

            case "FAIL":
                Debug.LogWarning("Lấy bảng xếp hạng thất bại!");
                break;

            default:
                Debug.LogWarning("Status không hợp lệ trong phản hồi ranking: " + jsonData);
                break;
        }
    }
}
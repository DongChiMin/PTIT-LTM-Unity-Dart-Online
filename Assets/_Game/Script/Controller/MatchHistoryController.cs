using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using TMPro;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class MatchHistoryController : Singleton<MatchHistoryController>
{
    [SerializeField] Transform tblMatchHistoryParent;
    [SerializeField] TblMatchHistoryContent TblMatchHistoryContentPrefab;

    public void ResetMatchHistory()
    {
        foreach (Transform child in tblMatchHistoryParent)
        {
            GameObject.Destroy(child.gameObject);
        }
    }

    public void AddMatchToHistory(int matchId, string startTime, string endTime, string opponentName, int yourScore, int opponentScore, string result, string note)
    {
        // Tạo mới một đối tượng TblMatchHistoryContent từ prefab
        TblMatchHistoryContent tblMatchHistoryContent = Instantiate(TblMatchHistoryContentPrefab, tblMatchHistoryParent);

        // Cập nhật các thông tin trận đấu
        tblMatchHistoryContent.matchId.text = matchId.ToString();
        tblMatchHistoryContent.startTime.text = startTime;
        tblMatchHistoryContent.endTime.text = endTime;
        tblMatchHistoryContent.opponentName.text = opponentName;
        tblMatchHistoryContent.yourScore.text = yourScore.ToString();
        tblMatchHistoryContent.opponentScore.text = opponentScore.ToString();
        tblMatchHistoryContent.result.text = result;
        tblMatchHistoryContent.note.text = note;

        // Có thể thay đổi màu sắc kết quả dựa vào kết quả trận đấu (thắng, thua, hòa)
        if (result == "WIN")
        {
            tblMatchHistoryContent.result.color = Color.green; // Màu xanh cho thắng
        }
        else if (result == "LOSE")
        {
            tblMatchHistoryContent.result.color = Color.red; // Màu đỏ cho thua
        }
        else
        {
            tblMatchHistoryContent.result.color = Color.gray; // Màu xám cho hòa
        }
    }
}

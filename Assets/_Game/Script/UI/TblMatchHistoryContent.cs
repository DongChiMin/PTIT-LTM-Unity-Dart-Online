using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TblMatchHistoryContent : MonoBehaviour
{
    public TextMeshProUGUI matchId;           // ID của trận đấu
    public TextMeshProUGUI startTime;         // Thời gian bắt đầu trận đấu
    public TextMeshProUGUI endTime;           // Thời gian kết thúc trận đấu
    public TextMeshProUGUI opponentName;      // Tên đối thủ
    public TextMeshProUGUI yourScore;         // Điểm của bạn
    public TextMeshProUGUI opponentScore;     // Điểm của đối thủ
    public TextMeshProUGUI result;            // Kết quả trận đấu (WIN/LOSE)
    public TextMeshProUGUI note;
}

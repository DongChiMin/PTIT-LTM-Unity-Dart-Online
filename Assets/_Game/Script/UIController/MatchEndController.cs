using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MatchEndController : Singleton<MatchEndController>
{
    [SerializeField] TextMeshProUGUI label;
    [SerializeField] TextMeshProUGUI yourScore;
    [SerializeField] TextMeshProUGUI opponentScore;
    [SerializeField] TextMeshProUGUI rankPoint;

    [SerializeField] Button tryAgain;
    [SerializeField] TextMeshProUGUI outPlayerName;

    private int opponentId;
    private string opponentName;
    private int rankPointInt;
    
    private void Start()
    {
        tryAgain.onClick.AddListener(() => PlayerOnlineListController.Instance.onClickInvite(opponentId, opponentName));
    }


    public void SetMatchEndContent(int yourScore, int opponentScore, int rankPoint, string label)
    {
        this.rankPointInt = rankPoint;
        string rankPointStr;
        rankPointStr = rankPoint >= 0 ? "+" + rankPoint : rankPoint.ToString();
        this.yourScore.text = "Điểm của bạn: " + yourScore;
        this.opponentScore.text = "Điểm của đối thủ: " + opponentScore;
        this.rankPoint.text = "rank point: " + rankPointStr;
        this.label.text = label;
    }

    public void SetOpponentAttribute(int id, string name, string reason)
    {
        opponentId = id;
        opponentName = name;
        if(reason == "player_left")
        {
            if(rankPointInt > 0)
            {
                outPlayerName.text = "Người chơi " + opponentName + " đã out";
            }
            else if(rankPointInt < 0)
            {
                outPlayerName.text = "Người chơi " + PlayerPrefs.GetString("user_playerName") + " đã out";
            }
        }
        else
        {
            outPlayerName.text = "";
        }
    }
}

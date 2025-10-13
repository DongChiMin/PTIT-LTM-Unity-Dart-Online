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

    private int opponentId;
    private string opponentName;
    
    private void Start()
    {
        tryAgain.onClick.AddListener(() => PlayerOnlineListController.Instance.onClickInvite(opponentId, opponentName));
    }


    public void SetMatchEndContent(int yourScore, int opponentScore, int rankPoint, string label)
    {
        string rankPointStr;
        rankPointStr = rankPoint >= 0 ? "+" + rankPoint : "-" + rankPoint;
        this.yourScore.text = "Điểm của bạn: " + yourScore;
        this.opponentScore.text = "Điểm của đối thủ: " + opponentScore;
        this.rankPoint.text = "rank point: " + rankPointStr;
        this.label.text = label;
    }

    public void SetOpponentAttribute(int id, string name)
    {
        opponentId = id;
        opponentName = name;
    }
}

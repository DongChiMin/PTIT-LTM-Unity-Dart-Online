using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class RoundController : Singleton<RoundController>
{


    [SerializeField] TextMeshProUGUI playerNameTurn;
    [SerializeField] TextMeshProUGUI round;

    [SerializeField] TMP_InputField scoreInput;
    [SerializeField] TMP_InputField forceInput;
    [SerializeField] Button sendScore;
    [SerializeField] Button sendForce;

    [SerializeField] TextMeshProUGUI opponentForceReceived;
    [SerializeField] TextMeshProUGUI yourScore;
    [SerializeField] TextMeshProUGUI opponentScore;

    private string playerP;
    private int matchId;

    public void SetRoundText(int matchId, string playerNameTurn, int round)
    {
        this.matchId = matchId;
        this.round.text = "Vòng đấu: " + round.ToString();
        this.playerNameTurn.text = "lượt của: " + playerNameTurn;
    }

    public void SetFields(bool forceField, bool scoreField)
    {
        forceInput.interactable = forceField;
        sendForce.interactable = forceField;

        scoreInput.interactable = scoreField;
        sendScore.interactable = scoreField;
    }

    public void SetOpponentForceReceived(float force)
    {
        opponentForceReceived.text = "Lực ném của đối thủ nhận được: " + force.ToString();
    }

    public void SetScore(int p1Score, int p2Score)
    {
        int yourScore;
        int opponentScore;
        if (playerP == "P1")
        {
            yourScore = p1Score;
            opponentScore = p2Score;
        }
        else
        {
            yourScore= p2Score;
            opponentScore = p1Score;
        }

        this.yourScore.text = "Điểm của bạn: " + yourScore.ToString();
        this.opponentScore.text = "Điểm của đối thủ: " + opponentScore.ToString();
    }

    public void OnClickSendScore()
    {
        ThrowScorePacket packet = new ThrowScorePacket(matchId, int.Parse(scoreInput.text));
        NetworkStream stream = ServerConnection.Instance.GetStream();
        PacketSender.SendPacket(packet, stream);

        //sau khi gửi điểm xong thì đến lượt đối thủ
        SetFields(false, false);
    }

    public void OnClickSendForce()
    {
        ThrowForcePacket packet = new ThrowForcePacket(matchId, float.Parse(forceInput.text));
        NetworkStream stream = ServerConnection.Instance.GetStream();
        PacketSender.SendPacket(packet, stream);

        //Sau khi gửi lực thì sẽ có kết quả điểm và gửi điểm
        SetFields(false, true);
    }

    public void OnClickEnd()
    {
        ExitMatchPacket packet = new ExitMatchPacket(PlayerPrefs.GetInt("user_id"));
        NetworkStream stream = ServerConnection.Instance.GetStream();
        PacketSender.SendPacket(packet, stream);
        
        UIManager.Instance.HideAll();
    }

    public void SetPlayerP(string playerP)
    {
        this.playerP = playerP;
    }
}

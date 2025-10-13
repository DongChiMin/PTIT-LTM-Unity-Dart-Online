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

    public void SetScore(int yourScore, int opponentScore)
    {
        this.yourScore.text = "Điểm của bạn: " + (int.Parse(this.yourScore.text) + yourScore).ToString();
        this.opponentScore.text = "Điểm của đối thủ: " + (int.Parse(this.opponentScore.text) + opponentScore).ToString();
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

    }
}

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

    public void OnClickSendScore()
    {
        ThrowScorePacket packet = new ThrowScorePacket(matchId, int.Parse(scoreInput.text));
        NetworkStream stream = ServerConnection.Instance.GetStream();
        PacketSender.SendPacket(packet, stream);
    }

    public void OnClickSendForce()
    {
        ThrowForcePacket packet = new ThrowForcePacket(matchId, float.Parse(forceInput.text));
        NetworkStream stream = ServerConnection.Instance.GetStream();
        PacketSender.SendPacket(packet, stream);
    }
}

using System.Collections;
using System.Collections.Generic;
using TMPro;
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

    public void SetRoundText(string playerNameTurn, string round)
    {
        this.round.text = "Vòng đấu: " + round;
        this.playerNameTurn.text = "lượt của: " + playerNameTurn;
    }

    public void DisableInteract()
    {
        scoreInput.interactable = false;
        forceInput.interactable = false;
        sendScore.interactable = false;
        sendForce.interactable = false;
    }

    public void EnableInteract()
    {
        scoreInput.interactable = true;
        forceInput.interactable = true;
        sendScore.interactable = true;
        sendForce.interactable = true;
    }
}

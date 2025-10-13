using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TblPlayerContent : MonoBehaviour
{
    public TextMeshProUGUI index;
    public TextMeshProUGUI playerName;
    public TextMeshProUGUI status;
    public Button inviteButton;
    private int playerId;

    public void SetPlayerId(int id)
    {
        playerId = id;
        inviteButton.onClick.AddListener(() => PlayerOnlineListController.Instance.onClickInvite(playerId, playerName.text));
    }

}

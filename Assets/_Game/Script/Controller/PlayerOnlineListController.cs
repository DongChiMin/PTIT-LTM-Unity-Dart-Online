using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using TMPro;
using UnityEngine;

public class PlayerOnlineListController : Singleton<PlayerOnlineListController>
{
    [SerializeField] Transform tblOnlineUsersParent;
    [SerializeField] TblPlayerContent TblPlayerContentPrefab;

    [SerializeField] TextMeshProUGUI waitingInviteText;
    [SerializeField] TextMeshProUGUI waitingAcceptText;
    [SerializeField] TextMeshProUGUI inviteFailedText;

    [SerializeField] SliderTimerUI sliderTimerInviter;
    [SerializeField] SliderTimerUI sliderTimerInvitee;

    private int inviteFromId = -1;
    private int inviteToId = -1;

    bool isfetching = false;

    private void Start()
    {
        isfetching = false;
    }

    private void Update()
    {
        if (UIManager.Instance.IsShown(UIPaneltype.playerOnlineList))
        {
            if (isfetching)
            {
                isfetching = false;
                StartCoroutine(UpdatePlayerOnlineList(2));
            }
            
        }
    }

    IEnumerator UpdatePlayerOnlineList(float time)
    {
        //Gửi lệnh lấy danh sách người choi online
        GetOnlineUsersPacket packet = new GetOnlineUsersPacket();
        NetworkStream stream = ServerConnection.Instance.GetStream();
        PacketSender.SendPacket(packet, stream);

        yield return new WaitForSeconds(time);
        isfetching = true;

    }
    public void onClickExit()
    {
        CameraManager.Instance.SetCamera(CameraType.mainMenu);
        UIManager.Instance.ShowOnly(UIPaneltype.mainMenu);
    }

    public void onClickInvite(int playerId, string playerName)
    {
        //Gửi lệnh invite về server
        InvitePacket packet = new InvitePacket(playerId);
        NetworkStream stream = ServerConnection.Instance.GetStream();
        PacketSender.SendPacket(packet, stream);

        inviteToId = playerId;

        //Hiển thị UI
        UIManager.Instance.Show(UIPaneltype.waitingInvite);
        SetWaitingInviteText("Waiting " + playerName + " to accept...");
        sliderTimerInviter.StartCountdown(10);
    }

    public void onClickCancelInvite()
    {
        //Gửi lệnh cancel invite về server
        CancelInvitePacket packet = new CancelInvitePacket(inviteToId);
        NetworkStream stream = ServerConnection.Instance.GetStream();
        PacketSender.SendPacket(packet, stream);

        UIManager.Instance.ShowOnly(UIPaneltype.playerOnlineList);
    }

    public void onClickAccept()
    {
        //Gửi lệnh invite response về server
        InviteResponsePacket packet = new InviteResponsePacket(inviteFromId, "ACCEPT");
        NetworkStream stream = ServerConnection.Instance.GetStream();
        PacketSender.SendPacket(packet, stream);

        //Vào màn hình chơi
        //UIManager.Instance.ShowOnly(UIPaneltype.playingDemo);
        GameManager.Instance.ChangeState(GameState.GamePlay);
        RoundController.Instance.SetPlayerP("P2");
        RoundController.Instance.ResetAttribute();
    }

    public void onClickDecline()
    {
        //Gửi lệnh invite response về server
        InviteResponsePacket packet = new InviteResponsePacket(inviteFromId, "DECLINE");
        NetworkStream stream = ServerConnection.Instance.GetStream();
        PacketSender.SendPacket(packet, stream);

        //Về màn hình danh sách người chơi online (có làm mới)
        MainMenuController.Instance.onClickPlay();
    }

    public void ResetOnlineUsersList()
    {
        foreach (Transform child in tblOnlineUsersParent)
        {
            GameObject.Destroy(child.gameObject);
        }
    }

    public void SetOnlineUsersList(int id, string index, string playerName, string status)
    {
        TblPlayerContent tblPlayerContent = Instantiate(TblPlayerContentPrefab, tblOnlineUsersParent);
        tblPlayerContent.index.text = index;
        tblPlayerContent.playerName.text = playerName;
        tblPlayerContent.status.text = status;
        //set id cho nút bấm
        tblPlayerContent.SetPlayerId(id);

        if (status != "online")
        {
            tblPlayerContent.inviteButton.gameObject.SetActive(false);
        }
    }

    public void SetWaitingInviteText(string msg)
    {
        waitingInviteText.text = msg;
    }

    public void SetWaitingAcceptText(string msg)
    {
        waitingAcceptText.text = msg;
    }

    public void SetInviteFailedText(string msg)
    {
        inviteFailedText.text = msg;
    }

    public void SetInviteFromId (int newId)
    {
        inviteFromId = newId;
    }

    public void SetIsFetching(bool isFetching)
    {
        this.isfetching = isFetching;
    }

    public SliderTimerUI GetSilderTimerInvitee()
    {
        return sliderTimerInvitee;
    }
}

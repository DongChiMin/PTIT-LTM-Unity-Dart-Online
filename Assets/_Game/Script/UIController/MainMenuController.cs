using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using TMPro;
using UnityEngine;

public class MainMenuController : Singleton<MainMenuController>
{
    [SerializeField] TextMeshProUGUI welcomeTMP;

    public void SetWelcomeTMP(string playerName)
    {
        welcomeTMP.text = "Welcome back, " + playerName + "!";
    }

    
    public void onClickPlay() {
        UIManager.Instance.ShowOnly(UIPaneltype.playerOnlineList);

        //Gửi lệnh lấy danh sách người choi online
        GetOnlineUsersPacket packet = new GetOnlineUsersPacket();
        NetworkStream stream = ServerConnection.Instance.GetStream();
        PacketSender.SendPacket(packet, stream);
    }

    public void onClickRanking()
    {

    }

    public void onClickExit()
    {
        Application.Quit();
    }

    public void OnClickLogout()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();
        UIManager.Instance.ShowOnly(UIPaneltype.login);
        LoginController.Instance.ResetInput();

        //Gửi lệnh logout về server
        LogoutPacket packet = new LogoutPacket();
        NetworkStream stream = ServerConnection.Instance.GetStream();
        PacketSender.SendPacket(packet, stream);

        //Kết nối lại
        ServerConnection.Instance.StartConnect();
    }
}

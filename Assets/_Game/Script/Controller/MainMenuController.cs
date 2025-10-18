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
        CameraManager.Instance.SetCamera(CameraType.onlineListMenu);
        UIManager.Instance.ShowOnly(UIPaneltype.playerOnlineList);

        //Gửi lệnh lấy danh sách người choi online
        GetOnlineUsersPacket packet = new GetOnlineUsersPacket();
        NetworkStream stream = ServerConnection.Instance.GetStream();
        PacketSender.SendPacket(packet, stream);
    }

    public void onClickRanking()
    {
        UIManager.Instance.ShowOnly(UIPaneltype.playerRanking);

        // 2. Gửi yêu cầu lấy danh sách bảng xếp hạng lên server
        GetRankingPacket packet = new GetRankingPacket();
        NetworkStream stream = ServerConnection.Instance.GetStream();
        PacketSender.SendPacket(packet, stream);
    }

    public void onClickExit()
    {
        Application.Quit();
    }

    public void OnClickLogout()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();
        CameraManager.Instance.SetCamera(CameraType.login);
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

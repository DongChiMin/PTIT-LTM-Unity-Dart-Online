using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using TMPro;
using UnityEngine;

public class LoginController : Singleton<LoginController>
{
    [SerializeField] TMP_InputField usernameInput;
    [SerializeField] TMP_InputField passwordInput;

    public void OnClickLogin()
    {
        LoginPacket login = new LoginPacket(usernameInput.text, passwordInput.text);
        NetworkStream stream = ServerConnection.Instance.GetStream();
        PacketSender.SendPacket(login, stream);
    }

    public void OnClickTryAgain()
    {
        CameraManager.Instance.SetCamera(CameraType.login);
        UIManager.Instance.ShowOnly(UIPaneltype.login);
    }

    public void OnClickRegister()
    {
        UIManager.Instance.ShowOnly(UIPaneltype.register);
    }

    public void ResetInput()
    {
        usernameInput.text = "";
        passwordInput.text = "";
    }
}

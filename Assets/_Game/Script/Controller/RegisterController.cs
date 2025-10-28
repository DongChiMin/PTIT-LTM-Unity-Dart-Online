using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using TMPro;
using UnityEngine;

public class RegisterController : MonoBehaviour
{
    [SerializeField] TMP_InputField usernameInput;
    [SerializeField] TMP_InputField passwordInput;
    [SerializeField] TMP_InputField playerName;

    public void OnClickRegister()
    {
        RegisterPacket packet = new RegisterPacket(usernameInput.text, passwordInput.text, playerName.text);
        NetworkStream stream = ServerConnection.Instance.GetStream();
        PacketSender.SendPacket(packet, stream);
    }

    public void OnClickBack()
    {
        UIManager.Instance.ShowOnly(UIPaneltype.login);
        LoginController.Instance.ResetInput();
    }

    public void OnClickTryAgain()
    {
        UIManager.Instance.ShowOnly(UIPaneltype.register);
    }
}

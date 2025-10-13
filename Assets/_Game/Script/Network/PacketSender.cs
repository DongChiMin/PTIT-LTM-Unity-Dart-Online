using System.IO;
using System.Net.Sockets;
using System.Text;
using UnityEngine;

public class PacketSender
{
    public static void SendPacket<T>(T packet, NetworkStream stream)
    {
        string json = JsonUtility.ToJson(packet);
        Debug.Log("Sending: " + json);

        byte[] bytes = Encoding.UTF8.GetBytes(json + "\n"); // \n dùng để server biết kết thúc gói
        try
        {
            stream.Write(bytes, 0, bytes.Length);
        }catch(IOException)
        {
            Debug.LogError("❌ SocketException khi gửi gói tin: " + json);
            UIManager.Instance.ShowOnly(UIPaneltype.connectionFailed);
        }
    }
}

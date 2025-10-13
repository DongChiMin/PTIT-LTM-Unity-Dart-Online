[System.Serializable]
public class InviteResponseData
{
    public int inviterId;
    public string response;  // "ACCEPT" hoặc "DECLINE"
}

[System.Serializable]
public class InviteResponsePacket : BasePacket
{
    public InviteResponseData data;

    public InviteResponsePacket(int inviterId, string response)
    {
        action = "invite_response";
        data = new InviteResponseData
        {
            inviterId = inviterId,
            response = response
        };
    }
}

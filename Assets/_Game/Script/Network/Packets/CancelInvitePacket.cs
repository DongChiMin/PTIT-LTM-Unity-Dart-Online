[System.Serializable]
public class CancelInviteData
{
    public int targetId;
}

[System.Serializable]
public class CancelInvitePacket : BasePacket
{
    public CancelInviteData data;

    public CancelInvitePacket(int targetUsername)
    {
        action = "cancel_invite";
        data = new CancelInviteData
        {
            targetId = targetUsername
        };
    }
}

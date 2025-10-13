[System.Serializable]
public class InviteData
{
    public int targetId;
}

[System.Serializable]
public class InvitePacket : BasePacket
{
    public InviteData data;

    public InvitePacket(int targetUsername)
    {
        action = "invite";
        data = new InviteData
        {
            targetId = targetUsername
        };
    }
}

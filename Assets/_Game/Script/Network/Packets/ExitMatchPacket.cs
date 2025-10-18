[System.Serializable]
public class ExitMatchData
{
    public int playerId;
}

[System.Serializable]
public class ExitMatchPacket : BasePacket
{
    public ExitMatchData data;

    public ExitMatchPacket(int playerId)
    {
        action = "exit_match";
        data = new ExitMatchData
        {
            playerId = playerId
        };
    }
}

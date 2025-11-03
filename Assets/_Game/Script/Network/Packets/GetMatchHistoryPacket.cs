[System.Serializable]
public class GetMatchHistoryPacket : BasePacket
{
    public GetMatchHistoryPacket()
    {
        action = "get_match_history";
    }
}

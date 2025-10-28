[System.Serializable]
public class GetRankingPacket : BasePacket
{
    public GetRankingPacket()
    {
        action = "get_ranking";
    }
}

//{
//    "action": "throw_score",
//  "data": {
//        "matchId": 101,
//    "score": 8
//  }
//}

[System.Serializable]
public class ThrowScoreData
{
    public int matchId;
    public int score;
}

[System.Serializable]
public class ThrowScorePacket : BasePacket
{
    public ThrowScoreData data;

    public ThrowScorePacket(int matchId, int score)
    {
        action = "throw_score";
        data = new ThrowScoreData
        {
            matchId = matchId,
            score = score
        };
    }
}

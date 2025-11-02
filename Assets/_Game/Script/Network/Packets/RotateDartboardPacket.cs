//{
//    "action": "rotate_dartboard", 
//  "data": {
//        "matchId": 101,
//      "speed" : 198.76,
//  }
//}

[System.Serializable]
public class RotateDartboardData
{
    public int matchId;
    public float speed;
}

[System.Serializable]
public class RotateDartboardPacket : BasePacket
{
    public RotateDartboardData data;

    public RotateDartboardPacket(int matchId, float speed)
    {
        action = "rotate_dartboard";
        data = new RotateDartboardData
        {
            matchId = matchId,
            speed = speed,
        };
    }
}

//{
//    "action": "throw_force", 
//  "data": {
//        "matchId": 101,
//    "force": 8
//  }
//}
[System.Serializable]
public class ThrowForceData
{
    public int matchId;
    public float force;
}

[System.Serializable]
public class ThrowForcePacket : BasePacket
{
    public ThrowForceData data;

    public ThrowForcePacket(int matchId, float force)
    {
        action = "throw_force";
        data = new ThrowForceData
        {
            matchId = matchId,
            force = force
        };
    }
}

//{
//  "action": "throw_swipe", 
//  "data": {
//      "matchId": 101,
//      "swipeX" : 198.76,
//      "swipeY: : 1801.23
//  }
//}
using UnityEngine;

[System.Serializable]
public class ThrowSwipeData
{
    public int matchId;
    public float swipeX;
    public float swipeY;
}

[System.Serializable]
public class ThrowSwipePacket : BasePacket
{
    public ThrowSwipeData data;

    public ThrowSwipePacket(int matchId, Vector2 swipe)
    {
        action = "throw_swipe";
        data = new ThrowSwipeData
        {
            matchId = matchId,
            swipeX = swipe.x,
            swipeY = swipe.y
        };
    }
}

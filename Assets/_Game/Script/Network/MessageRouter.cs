using System;
using UnityEngine;
using SimpleJSON;
public class MessageRouter : Singleton<MessageRouter>
{
    private LoginHandler loginHandler = new LoginHandler();
    private OnlineUsersHandler onlineUsersHandler = new OnlineUsersHandler();
    private InviteHandler inviteHandler = new InviteHandler();
    private InviteResponseHandler inviteResponseHandler = new InviteResponseHandler();
    private RoundStartHandler roundStartHandler = new RoundStartHandler();
    private MatchEndHandler matchEndHandler = new MatchEndHandler();
    public void Route(string msg)
    {
        // Tách chuỗi theo dấu xuống dòng (\n) để lấy các JSON riêng biệt
        var jsonStrings = msg.Trim().Split('\n');
        if(jsonStrings.Length > 1)
        {
            Debug.Log("TÁCH ĐƯỢC RỒI");
        }
        // Lặp qua từng chuỗi JSON
        foreach (var jsonString in jsonStrings)
        {
            var json = JSON.Parse(jsonString.Trim());
            string response = json["response"];

            switch (response)
            {
                case "response_login":
                    loginHandler.Handle(json);
                    break;
                case "response_register":

                    break;

                case "response_online_users":
                    onlineUsersHandler.Handle(json);
                    break;
                case "response_invite":
                    inviteHandler.Handle(json);
                    break;
                case "response_invite_response":
                    inviteResponseHandler.Handle(json);
                    break;
                case "response_round_start":
                    //{ "response":"response_round_start","status":"SUCCESS","data":{ "matchId":3,"round":1,"firstTurnId":3,"firstTurnName":"nguyet"} }
                    roundStartHandler.Handle(json);
                    break;
                case "response_throw_force":
                    //{ "response":"response_throw_force","status":"SUCCESS","data":{ "matchId":1,"round":1,"playerId":2,"playerName":"hoang","force":0.0,"timeout":false} }
                    roundStartHandler.HandleThrowForce(json);
                    break;
                case "response_throw_score":
                    //P1 la người mời 
                    //{ "response":"response_throw_score","status":"SUCCESS","data":{ "matchId":4,"round":1,"playerId":2,"playerName":"hoang","score":34,"totalScoreP1":34,"totalScoreP2":0,"timeout":false} }
                    roundStartHandler.HandleRoundScore(json);
                    break;
                case "round_end":
                    //{ "response":"round_end","status":"SUCCESS","data":{ "matchId":2,"round":5,"totalScoreP1":170,"totalScoreP2":50} }


                    break;
                case "response_round_result":
                    break;
                case "response_match_end":
                    //{ "response":"response_match_end","status":"SUCCESS","data":{ "matchId":2,"idP1":2,"p1":"hoang","idP2":3,"p2":"nguyet","scoreP1":170,"scoreP2":50,"winner":"hoang"} }
                    matchEndHandler.Handle(json);
                    break;
                case "response_exit_match":
                    break;
                case "response_history":
                    break;
                case "response_ranking":
                    break;
                case "response_player_detail":
                    break;

                default:
                    Debug.LogWarning("Không biết xử lý: " + msg);
                    break;
            }
        }
    }
}

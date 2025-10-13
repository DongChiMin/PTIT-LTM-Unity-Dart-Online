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

    public void Route(string msg)
    {
        var json = JSON.Parse(msg.Trim());
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
                //{ "response":"response_throw_force","status":"SUCCESS","data":{ "matchId":1,"round":1,"playerId":2,"playerName":"hoang","force":0.0} }
                
                break;
            case "response_throw_score":
                //{ "response":"response_throw_score","status":"SUCCESS","data":{ "matchId":1,"round":1,"playerId":2,"playerName":"hoang","score":3,"totalScoreP1":3,"totalScoreP2":0,"timeout":false} }
                
                break;
            case "response_round_result":
                break;
            case "response_match_detail":
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

using System;
using UnityEngine;
using SimpleJSON;
public class MessageRouter : Singleton<MessageRouter>
{
    private LoginHandler loginHandler = new LoginHandler();
    private OnlineUsersHandler onlineUsersHandler = new OnlineUsersHandler();
    private InviteHandler inviteHandler = new InviteHandler();
    //private InviteResponseHandler inviteResponseHandler = new InviteResponseHandler();
    //private RoundStartHandler roundStartHandler = new RoundStartHandler();
    //private MatchEndHandler matchEndHandler = new MatchEndHandler();
    private RankingHandler rankingHandler = new RankingHandler();
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
            ////case "response_invite":
            ////    inviteHandler.Handle(json);
            ////    break;
            ////case "response_invite_response":
            ////    inviteResponseHandler.Handle(json);
            //    break;
            ////case "response_round_start":
            ////    //{ "response":"response_round_start","status":"SUCCESS","data":{ "matchId":3,"round":1,"firstTurnId":3,"firstTurnName":"nguyet"} }
            ////    roundStartHandler.Handle(json);
            ////    break;
            ////case "response_throw_force":
            ////    //{ "response":"response_throw_force","status":"SUCCESS","data":{ "matchId":1,"round":1,"playerId":2,"playerName":"hoang","force":0.0,"timeout":false} }
            ////    roundStartHandler.HandleThrowForce(json);
            ////    break;
            ////case "response_throw_score":
            ////    //P1 la người mời 
            ////    //{ "response":"response_throw_score","status":"SUCCESS","data":{ "matchId":4,"round":1,"playerId":2,"playerName":"hoang","score":34,"totalScoreP1":34,"totalScoreP2":0,"timeout":false} }
            ////    roundStartHandler.HandleRoundScore(json);
            ////    break;
            ////case "round_end":
            ////    //{ "response":"round_end","status":"SUCCESS","data":{ "matchId":2,"round":5,"totalScoreP1":170,"totalScoreP2":50} }


            ////    break;
            ////case "response_round_result":
            ////    break;
            ////case "response_match_end":
            ////    //{ "response":"response_match_end","status":"SUCCESS","data":{ "matchId":2,"idP1":2,"p1":"hoang","idP2":3,"p2":"nguyet","scoreP1":170,"scoreP2":50,"winner":"hoang"} }
            ////    matchEndHandler.Handle(json);
            //    break;
            case "response_exit_match":
                break;
            case "response_history":
                break;
            case "response_ranking":
                rankingHandler.Handle(json);
                break;
            case "response_player_detail":
                break;

            default:
                Debug.LogWarning("Không biết xử lý: " + msg);
                break;
        }
    }
}
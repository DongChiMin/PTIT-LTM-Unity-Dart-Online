using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoginHandler
{
    public LoginHandler()
    {

    }

    public void Handle(SimpleJSON.JSONNode jsonData)
    {
        string status = jsonData["status"].Value;
        switch (status)
        {
            case "SUCCESS":
                //Chuyển sang UI Main Menu
                UIManager.Instance.ShowOnly(UIPaneltype.mainMenu);

                //Lấy data từ JSON
                int playerId = jsonData["data"]["id"];
                string username = jsonData["data"]["username"].Value;
                string playerName = jsonData["data"]["playerName"].Value;

                //Lưu thông tin người chơi vào PlayerPref
                PlayerPrefs.SetInt("user_id", playerId);
                PlayerPrefs.SetString("user_username", username);
                PlayerPrefs.SetString("user_playerName", playerName);
                PlayerPrefs.Save();

                MainMenuController.Instance.SetWelcomeTMP(playerName);
                break;
            case "FAIL":
                UIManager.Instance.Show(UIPaneltype.loginFailed);
                break;
            default:
                Debug.Log("Lỗi status của dữ liệu:" + jsonData);
                break;
        }
    }
}

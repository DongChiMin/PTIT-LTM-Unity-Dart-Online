using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RegisterHandler
{
    public RegisterHandler()
    {

    }

    public void Handle(SimpleJSON.JSONNode jsonData)
    {
        string status = jsonData["status"].Value;
        switch (status)
        {
            case "SUCCESS":
                UIManager.Instance.ShowOnly(UIPaneltype.login);
                break;
            case "FAIL":
                UIManager.Instance.Show(UIPaneltype.registerFailed);
                break;
            default:
                Debug.Log("Lỗi status của dữ liệu:" + jsonData);
                break;
        }
    }
}

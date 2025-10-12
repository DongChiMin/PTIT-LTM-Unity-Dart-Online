using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MainMenuController : Singleton<MainMenuController>
{
    [SerializeField] TextMeshProUGUI welcomeTMP;

    public void setWelcomeTMP(string playerName)
    {
        welcomeTMP.text = "Welcome back, " + playerName + "!";
    }

    public void onClickExit()
    {
        Application.Quit();
    }

    public void onClickLogout()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();
        UIManager.Instance.ShowOnly(UIPaneltype.login);
    }
}

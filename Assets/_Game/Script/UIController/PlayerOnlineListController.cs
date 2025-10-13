using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerOnlineListController : Singleton<PlayerOnlineListController>
{
    [SerializeField] Transform tblOnlineUsersParent;
    [SerializeField] TblPlayerContent TblPlayerContentPrefab;
    public void onClickExit()
    {
        UIManager.Instance.ShowOnly(UIPaneltype.mainMenu);
    }

    public void SetOnlineUsersList(string index, string playerName, string status)
    {
        TblPlayerContent tblPlayerContent = Instantiate(TblPlayerContentPrefab, tblOnlineUsersParent);
        tblPlayerContent.index.text = index;
        tblPlayerContent.playerName.text = playerName;
        tblPlayerContent.status.text = status;

        if(status != "online")
        {
            tblPlayerContent.inviteButton.SetActive(false);
        }
    }
}

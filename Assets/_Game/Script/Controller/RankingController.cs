using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using TMPro;
using UnityEngine;

public class RankingController : Singleton<RankingController>
{
    [SerializeField] Transform tblRankingParent;
    [SerializeField] TblPlayerRanking TblPlayerRankingPrefab;

    public void ResetRankingList()
    {
        foreach (Transform child in tblRankingParent)
        {
            GameObject.Destroy(child.gameObject);
        }
    }

    public void SetPlayerRanking(int rank, string playerName, int totalScore)
    {
        TblPlayerRanking content = Instantiate(TblPlayerRankingPrefab, tblRankingParent);
        content.playerName.text = playerName;
        content.ranking.text = rank.ToString();
        content.totalScore.text = totalScore.ToString();
    }

    public void OnClickExit()
    {
        CameraManager.Instance.SetCamera(CameraType.mainMenu);
        UIManager.Instance.ShowOnly(UIPaneltype.mainMenu);
    }
}

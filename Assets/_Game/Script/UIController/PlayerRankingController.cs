using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerRankingController : Singleton<PlayerRankingController>
{
    [Header("UI Elements")]
    [SerializeField] private Transform rankingContentParent;
    [SerializeField] private RankingRowUI rankingRowPrefab;
    [SerializeField] private TMP_Text currentUserRankText;
    [SerializeField] private Button exitButton;

    private int currentUserId = -1;

    private void Awake()
    {
        // Gán sự kiện cho nút Exit nếu có
        if (exitButton != null)
            exitButton.onClick.AddListener(OnClickExit);
    }

    /// <summary>
    /// Xóa toàn bộ danh sách hiện tại
    /// </summary>
    public void ResetRankingList()
    {
        foreach (Transform child in rankingContentParent)
        {
            Destroy(child.gameObject);
        }
    }

    /// <summary>
    /// Tạo một hàng mới trong bảng xếp hạng
    /// </summary>
    public void AddRankingRow(int rank, string playerName, int score, int playerId)
    {
        RankingRowUI newRow = Instantiate(rankingRowPrefab, rankingContentParent);
        newRow.SetData(rank, playerName, score);
        newRow.SetHighlight(playerId == currentUserId);
        newRow.gameObject.SetActive(true);
    }

    /// <summary>
    /// Hiển thị hạng của người chơi hiện tại
    /// </summary>
    public void SetCurrentUserRank(int rank)
    {
        if (currentUserRankText != null)
            currentUserRankText.text = $"Your Rank: {rank}";
    }

    /// <summary>
    /// Handler sẽ gọi hàm này để gán ID người dùng, phục vụ cho việc highlight
    /// </summary>
    public void SetCurrentUserId(int userId)
    {
        currentUserId = userId;
    }

    private void OnClickExit()
    {
        // Tắt panel này hoặc chuyển về menu chính
        gameObject.SetActive(false);
        // Hoặc: UIManager.Instance.ShowOnly(UIPaneltype.mainMenu);
    }
}
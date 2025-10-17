using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RankingRowUI : MonoBehaviour
{
    // Kéo thả các component từ Inspector của Prefab vào đây
    [SerializeField] private TMP_Text rankText;
    [SerializeField] private TMP_Text playerNameText;
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private Image backgroundImage;

    /// <summary>
    /// Hàm chính để điền dữ liệu từ controller vào UI của hàng này
    /// </summary>
    public void SetData(int rank, string playerName, int score)
    {
        rankText.text = rank.ToString();
        playerNameText.text = playerName;
        scoreText.text = score.ToString();
    }

    /// <summary>
    /// Bật/tắt highlight cho hàng của người chơi hiện tại
    /// </summary>
    public void SetHighlight(bool isHighlighted)
    {
        if (backgroundImage != null)
        {
            // Nếu isHighlighted là true, đổi màu; nếu false, trả về màu trắng
            backgroundImage.color = isHighlighted ? new Color(0.8f, 0.9f, 1f, 1f) : Color.white;
        }
    }
}
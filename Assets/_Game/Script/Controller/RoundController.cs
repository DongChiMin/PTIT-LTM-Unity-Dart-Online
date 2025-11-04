using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class RoundController : Singleton<RoundController>
{
    [SerializeField] TextMeshProUGUI playerNameTurn;
    [SerializeField] TextMeshProUGUI round;

    [SerializeField] TMP_InputField scoreInput;
    [SerializeField] TMP_InputField forceInput;
    [SerializeField] Button sendScore;
    [SerializeField] Button sendForce;

    [SerializeField] TextMeshProUGUI opponentForceReceived;
    [SerializeField] TextMeshProUGUI yourScore;
    [SerializeField] TextMeshProUGUI opponentScore;

    [SerializeField] GameObject opponentThrowingUI;
    [SerializeField] Dartboard dartboard;
    [SerializeField] Button rotateButton;

    //Xử lý thời gian
    [SerializeField] TextMeshProUGUI timeOutText;
    private float timeRemaining;
    private Coroutine countdownCoroutine;


    private string playerP;
    private int matchId;

    public void SetRoundText(int matchId, string playerNameTurn, int round, long timeOut)
    {
        this.matchId = matchId;
        this.round.text = "Vòng đấu: " + round.ToString();
        this.playerNameTurn.text = "lượt của: " + playerNameTurn;

        //Xử lý thời gian
        timeRemaining = timeOut / 1000f;
        // Nếu đang có một Coroutine đang chạy, hủy nó đi trước khi bắt đầu một Coroutine mới
        if (countdownCoroutine != null)
        {
            StopCoroutine(countdownCoroutine);
        }
        // Bắt đầu Coroutine đếm ngược
        countdownCoroutine = StartCoroutine(CountdownTimer());
    }

    private IEnumerator CountdownTimer()
    {
        while (timeRemaining > 0)
        {
            // Hiển thị thời gian còn lại (có thể là giây hoặc phút)
            timeOutText.text = "Thời gian còn lại: " + Mathf.Ceil(timeRemaining).ToString() + "s";

            // Giảm thời gian còn lại theo mỗi giây
            timeRemaining -= 1f;

            if(timeRemaining < 10)
            {
                timeOutText.color = Color.red;
            }
            else
            {
                timeOutText.color = Color.black;
            }

                // Chờ một giây trước khi cập nhật lại
                yield return new WaitForSeconds(1f);
        }

        // Khi hết thời gian, cập nhật UI 
        timeOutText.text = "Hết thời gian!";
    }

    public void SetFields(bool forceField, bool scoreField)
    {
        forceInput.interactable = forceField;
        sendForce.interactable = forceField;

        scoreInput.interactable = scoreField;
        sendScore.interactable = scoreField;
    }

    public void ResetAttribute()
    {
        scoreInput.text = "";
        forceInput.text = "";
        opponentForceReceived.text = "";
        yourScore.text = "0";
        opponentScore.text = "0";
    }

    public void SetOpponentForceReceived(float force)
    {
        if(force < 0)
        {
            opponentForceReceived.text = "Đang trong lượt ném của bạn";
        }
        else
        {
            opponentForceReceived.text = "Lực ném của đối thủ nhận được: " + force.ToString();
        }
        
    }

    public void SetScore(int p1Score, int p2Score)
    {
        int yourScore;
        int opponentScore;
        if (playerP == "P1")
        {
            yourScore = p1Score;
            opponentScore = p2Score;
        }
        else
        {
            yourScore= p2Score;
            opponentScore = p1Score;
        }

        this.yourScore.text = yourScore.ToString();
        this.opponentScore.text = opponentScore.ToString();
    }

    public void OnClickSendScore()
    {
        ThrowScorePacket packet = new ThrowScorePacket(matchId, int.Parse(scoreInput.text));
        NetworkStream stream = ServerConnection.Instance.GetStream();
        PacketSender.SendPacket(packet, stream);

        //sau khi gửi điểm xong thì đến lượt đối thủ
        SetFields(false, false);
    }

    public void OnClickRotateDartboard()
    {
        RotateDartboardPacket packet = new RotateDartboardPacket(matchId, 100f);
        NetworkStream stream = ServerConnection.Instance.GetStream();
        PacketSender.SendPacket(packet, stream);

        //sau khi gửi điểm xong thì chờ server phản hồi mới xoay bia
    }

    public void OnClickSendForce()
    {
        ThrowForcePacket packet = new ThrowForcePacket(matchId, float.Parse(forceInput.text));
        NetworkStream stream = ServerConnection.Instance.GetStream();
        PacketSender.SendPacket(packet, stream);

        //Sau khi gửi lực thì sẽ có kết quả điểm và gửi điểm
        SetFields(false, true);
    }

    public void OnClickEnd()
    {
        ExitMatchPacket packet = new ExitMatchPacket(PlayerPrefs.GetInt("user_id"));
        NetworkStream stream = ServerConnection.Instance.GetStream();
        PacketSender.SendPacket(packet, stream);
        
        UIManager.Instance.HideAll();
    }

    public void SetPlayerP(string playerP)
    {
        this.playerP = playerP;
    }

    public void SendScore(int score)
    {
        ThrowScorePacket packet = new ThrowScorePacket(matchId, score);
        NetworkStream stream = ServerConnection.Instance.GetStream();
        PacketSender.SendPacket(packet, stream);
    }

    public void SendSwipe(Vector2 swipe)
    {
        ThrowSwipePacket packet = new ThrowSwipePacket(matchId, swipe);
        NetworkStream stream = ServerConnection.Instance.GetStream();
        PacketSender.SendPacket(packet, stream);
    }

    public void SetOpponentThrowingUI(bool boolean)
    {
        opponentThrowingUI.SetActive(boolean);
        if (!boolean)
        {
            this.playerNameTurn.color = Color.white;
        }
    }

    public void SetDartboardRotateSpeed(float speed)
    {
        dartboard.SetRotationSpeed(speed);
    }

    public void SetRotateButton(bool boolean)
    {
        rotateButton.gameObject.SetActive(boolean);
    }

    public Dartboard GetDartboard()
    {
        return dartboard;
    }
}

using UnityEngine;
using UnityEngine.UI;

public class SliderTimerUI : MonoBehaviour
{
    public Slider slider;    // Tham chiếu tới slider UI
    public float duration = 5f; // Thời gian đếm ngược (t giây)

    private float timer = 10f;
    private bool isRunning = false;

    void Start()
    {
        if (slider == null)
            slider = GetComponent<Slider>();

    }

    public void StartCountdown(float newDuration)
    {
        duration = newDuration;
        slider.value = 1f;
        timer = 0;
        isRunning = true;
    }

    void Update()
    {
        if (!isRunning) return;

        timer += Time.deltaTime;
        float progress = Mathf.Clamp01(timer / duration); // Từ 0 đến 1
        slider.value = 1f - progress;

        if (progress >= 1f)
        {
            isRunning = false;
            // Gọi callback hoặc sự kiện khi hoàn tất nếu cần
            if (UIManager.Instance.IsShown(UIPaneltype.waitingInvite))
            {
                UIManager.Instance.Hide(UIPaneltype.waitingInvite);
                UIManager.Instance.Show(UIPaneltype.inviteTimeout);
            }
            else if (UIManager.Instance.IsShown(UIPaneltype.waitingAccept))
            {
                UIManager.Instance.ShowOnly(UIPaneltype.playerOnlineList);
            }
        }
    }
}

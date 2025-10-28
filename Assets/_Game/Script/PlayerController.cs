using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEngine;

public class PlayerController : Singleton<PlayerController>
{
    DartManager dartManager;
    Dart currentDart;

    //Biến kiểm tra mình có phải là người ném ko
    bool isThrower;

    //Touch Control
    private Vector2 startPos;
    private Vector2 endPos;
    void Start()
    {
        dartManager = DartManager.Instance;
        currentDart = dartManager.GetCurrentDart();
        isThrower = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (isThrower && currentDart.GetCurrentState() != DartState.Hit)
        {
            SwipeCheck();
        }
    }

    private void SwipeCheck()
    {
        if (Input.GetMouseButtonDown(0))
        {
            startPos = Input.mousePosition;
        }

        if (Input.GetMouseButtonUp(0))
        {
            endPos = Input.mousePosition;
            Vector2 swipe = endPos - startPos;

            // Vuốt từ trên xuống => bỏ qua
            if (swipe.y <= 0f)
            {
                Debug.Log("Vuốt xuống - không ném");
                return;
            }

            //Gửi tin nhắn cho server
            RoundController.Instance.SendSwipe(swipe);

            //Thực hiện logic ném dựa theo vuốt sẽ được xử lý khi nhận được tin nhắn từ server
            //ShootDart(swipe);
        }
    }

    public void ShootDart(Vector2 swipe)
    {
        currentDart.Shoot(swipe);
    }

    public void SetDart(Dart dart)
    {
        currentDart = dart;
    }

    public void SetIsThrower(bool boolean)
    {
        isThrower = boolean;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
 
    }
}

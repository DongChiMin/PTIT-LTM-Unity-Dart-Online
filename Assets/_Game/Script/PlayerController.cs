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

    //Touch Controll
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
        // --- GHI LẠI VỊ TRÍ VUỐT ---
        if (currentDart != null) {
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

                currentDart.Shoot(swipe);
                StartCoroutine(ReloadDart(1f));
            }
        }
    }

    public void ShootDart(Vector2 swipe)
    {
        currentDart.Shoot(swipe);
        StartCoroutine(ReloadDart(1f));
    }

    IEnumerator ReloadDart(float time)
    {
        yield return new WaitForSeconds(time);
        DartManager.Instance.ReloadDart();
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

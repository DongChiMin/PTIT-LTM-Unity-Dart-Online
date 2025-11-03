using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Burst.CompilerServices;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public enum DartState
{
    Ready,
    Flying,
    Hit
}

public class Dart : MonoBehaviour
{
    [Header("Thuoc tinh")]
    [SerializeField] float flyForce;
    [SerializeField] float gravityForce;
    [SerializeField] float rotationSpeed;

    [Header("Do lech khi nem phi tieu")]
    [Range(-100f, 100f)]
    public float maxAngleX;

    [Range(-100f, 100f)]
    public float minAngleX;

    [Range(-100f, 100f)]
    public float maxAngleY;

    [Range(-100f, 100f)]
    public float minAngleY;

    Rigidbody rb;
    private DartState dartState;
    public event Action<DartState> OnStateChanged;

    //Nếu đang đến lượt mình ném thì isThrower=true
    //nếu isThrower=true thì mới gửi thông tin điểm đến server
    private bool isThrower;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        OnInit();
    }

    void OnInit()
    {
        rb.useGravity = false;
        ChangeState(DartState.Ready);
    }

    private void FixedUpdate()
    {
        if(dartState == DartState.Flying)
        {
            if (rb.velocity != Vector3.zero)
            {
                //Nhìn về hướng velocity (hướng bay) của phi tiêu để quay dần dần về hướng đó
                Quaternion targetRotation = Quaternion.LookRotation(rb.velocity.normalized);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
            }

            //Tăng trọng lực cho phi tiêu chúi xuống nhanh hơn
            rb.AddForce(Vector3.down * gravityForce, ForceMode.Acceleration);
        }
    }

    public void Shoot(Vector2 swipe)
    {
        // Tính hướng vuốt (normalized)
        Vector2 dir = swipe.normalized;

        // -------------------------
        // ÁNH XẠ GÓC NGANG (Y)
        // -------------------------
        // Vuốt trái/phải -> thay đổi angleY (angleY là trái phải)
        // dir.x = -1 (trái) -> angleY = minAngleY
        // dir.x =  1 (phải) -> angleY = maxAngleY
        float normalizedX = Mathf.InverseLerp(-Screen.width / 2, Screen.width / 2, swipe.x);
        normalizedX = Mathf.Clamp01(normalizedX);
        float mappedAngleY = Mathf.Lerp(minAngleY, maxAngleY, normalizedX);

        // -------------------------
        // ÁNH XẠ GÓC DỌC (X)
        // -------------------------
        // Vuốt từ dưới lên -> swipe.y càng lớn -> angleX càng gần maxAngleX (angleX là lên xuống)


        // InverserLerp = (value - a ) / (b-a)
        // Giá trị của swipe.y chỉ được trải từ [0, +chiều dọc màn hình/2] vì chỉ vuốt lên, cần chuẩn hóa về [0,1] dùng InverseLerp
        // Giá trị của swipe.y có thể vượt quá giới hạn nếu vuốt ra ngoài màn hình --> Cần dùng clamp
        // 50px = vuốt ngắn nhất → 0, 500px = vuốt dài nhất → 1
        float normalizedY = Mathf.InverseLerp(0, Screen.height / 2, swipe.y);

        // Clamp01 giới hạn giá trị normalizedY trong khoảng [0,1] 
        // Trường hợp vuốt quá ngắn (<50px) → kết quả âm (10px --> normalizedY = -0.088), clamp về 0
        // Trường hợp vuốt quá dài (>500px) → kết quả >1, clamp về 1
        normalizedY = Mathf.Clamp01(normalizedY);

        // mappedAngle = a+(b−a)*t : (nội suy tuyến tinh)
        // trong đó t là giá trị chuẩn hóa 0-1, b là max, a là min
        //  (VD t = 0 --> mapped = min (nếu vuốt ngắn thì ném góc a))
        //normalizedY càng lớn --> càng gần minAngleX --> Phi tiêu càng ngửa lên cao (vì dựa theo rotation.x)
        float mappedAngleX = Mathf.Lerp(maxAngleX, minAngleX, normalizedY);


        // Quay rotation của phi tiêu.
        Quaternion deviationRotation = Quaternion.Euler(mappedAngleX, mappedAngleY, 0);
        Vector3 shootDirection = deviationRotation * Vector3.forward;

        // Reset trạng thái vật lý
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        rb.useGravity = true;

        // Bắn theo hướng đã xoay
        rb.AddForce(shootDirection * flyForce, ForceMode.Impulse);

        //Đổi state
        ChangeState(DartState.Flying);

        // Debug
        Debug.Log($"Shoot! swipe=({swipe.x:F1},{swipe.y:F1}) -> angleX={mappedAngleX:F1}, angleY={mappedAngleY:F1}");
    }

    private void OnTriggerEnter(Collider other)
    {
        string multiplier = "normal;0";
        Debug.Log(other.gameObject);
        if (other.gameObject.layer == LayerMask.NameToLayer("Multiplier"))
        {         
            switch (other.tag)
            {
                case "InnerBullEye":
                    Debug.Log("50 diem");
                    multiplier = "plus;50";
                    break;
                case "OuterBullEye":
                    Debug.Log("25 diem");
                    multiplier = "plus;25";
                    break;
                case "Treble3x":
                    Debug.Log("Nhan 3 so diem");
                    multiplier = "mul;3";
                    break;
                case "Treble2x":
                    Debug.Log("Nhan 2 so diem");
                    multiplier = "mul;2";
                    break;
                case "Single":
                    Debug.Log("Giu nguyen diem");
                    multiplier = "mul;1";
                    break;
            }
        }
        //Chuyển trạng thái của phi tiêu thành hit
        ChangeState(DartState.Hit);
        Hit();

        //Dừng xoay bia
        RoundController.Instance.SetDartboardRotateSpeed(0);

        //Kiểm tra va chạm
        Vector3 from = transform.position + transform.forward * 3f;
        Vector3 direction = transform.forward * -1f;


        Debug.DrawRay(from, direction * 6f, Color.red, 1f);
        RaycastHit hit;
        if (Physics.Raycast(from, direction, out hit, 6f, LayerMask.GetMask("Score")))
        {
            Debug.Log("Raycast hit at: " + hit.point);
            CalculateScore(hit.point, other.gameObject.transform, multiplier);
        }
        else
        {
            ScoreTextController.Instance.TrigerText(transform.position + new Vector3(0f, 0f, -3f), "0");
            if (isThrower) StartCoroutine(SendScoreDelay(2f, 0));
        }
    }

    void Hit()
    {
        //Khi bắn trúng đích thì dừng lại + tắt trọng lực
        Collider collider = gameObject.GetComponent<BoxCollider>();
        collider.enabled = false;
        rb.velocity = Vector3.zero;
        rb.useGravity = false;
    }

    void ChangeState(DartState newState) 
    {
        dartState = newState;
        OnStateChanged?.Invoke(dartState);
    }

    public DartState GetCurrentState()
    {
        return dartState;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        Vector3 origin = transform.position;
        Vector3 direction = transform.forward * -1f;  // ❗️KHÔNG nên dùng trong 2D

        float distance = 0.1f;
        Vector3 end = origin + direction.normalized * distance;

        Gizmos.DrawLine(origin, end);
    }

    //PHẦN TÍNH ĐIỂM
    int[] sectorScores = new int[20]
    {
        6, 13, 4, 18, 1,
        20, 5, 12, 9, 14,
        11, 8, 16, 7, 19,
        3, 17, 2, 15, 10
    };

    void CalculateScore(Vector3 hitPoint, Transform dartBoard, string multiplier)
    {
        //1. TÍNH VÒNG ĐIỂM
        //Kiểm tra nếu dartboard đã xoay thì cộng thêm góc xoay
        Dartboard dartboard = RoundController.Instance.GetDartboard();
        float rotateAngle = dartboard.transform.eulerAngles.z;
        Debug.Log(rotateAngle);

        Vector3 center = dartBoard.position;
        Vector3 dir = hitPoint - center;

        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - rotateAngle;
        while (angle < 0)
        {
            angle += 360;
        }
        Debug.Log("Angle:" + angle);

        //Hiện tại trong logic 0 độ --> 18 dộ là ô điểm thứ nhất. Nhưng thực tế -9 độ --> 9 độ mới là ô điểm thứ nhất
        //Giải pháp: offset cộng 9 độ để từ -9 đến 9 là ô điểm thứ nhất
        angle = (angle + 9f) % 360f;

        int numberOfSectors = 20;
        float sectorAngle = 360f / numberOfSectors;

        int sectorIndex = Mathf.FloorToInt(angle / sectorAngle);
        int score = sectorScores[sectorIndex]; // Mảng điểm của từng lát pizza

        //2. HỆ SỐ ĐIỂM (các vùng thưởng điểm)
        float distance = dir.magnitude;
        Debug.Log(distance + " " + dartboard.transform.localScale.x);
        if (distance <= 0.0542f * dartBoard.transform.localScale.x)
        {
            // Bullseye
            score = 50;
        }
        else if (distance <= 0.1206f * dartBoard.transform.localScale.x)
        {
            // Outer bull
            score = 25;
        }
        else if (distance <= 0.7076f * dartBoard.transform.localScale.x)
        {
            // Vòng đơn đầu tiên
            // giữ nguyên
        }
        else if (distance <= 0.7797f * dartBoard.transform.localScale.x)
        {
            // Vòng triple
            score *= 3;
        }
        else if (distance <= 1.17f * dartBoard.transform.localScale.x)
        {
            // Vòng đơn ngoài
            // giữ nguyên
        }
        else if (distance <= 1.26f * dartBoard.transform.localScale.x)
        {
            // Vòng double
            score *= 2;
        }
        else
        {
            // Ngoài bảng -> không tính điểm
            score = 0;
        }

        ScoreTextController.Instance.TrigerText(transform.position + new Vector3(0f, 0f, -3f), score.ToString());

        //Gửi điểm về server
        if(isThrower) StartCoroutine(SendScoreDelay(2f, score));
    }

    IEnumerator SendScoreDelay(float time, int score)
    {
        yield return new WaitForSeconds(time);
        RoundController.Instance.SendScore(score);
    }

    public void SetIsThrower(bool boolean)
    {
        isThrower = boolean;
    }


}

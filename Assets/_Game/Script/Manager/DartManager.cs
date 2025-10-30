using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DartManager : Singleton<DartManager>
{
    [SerializeField] Stack<Dart> darts = new Stack<Dart>();
    [SerializeField] Dart redDartPrefab;
    [SerializeField] Dart greenDartPrefab;
    Dart currentDart;
    [SerializeField] ObjectPooling dartPool;

    [SerializeField] float timeToDestroyHitDart;

    [SerializeField] PlayerController playerController;
    bool isHit;

    // Start is called before the first frame update
    void Start()
    {
        OnInit();
    }

    public void OnInit()
    {
        // Lặp qua tất cả các gameObject con và xóa chúng
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject); // Xóa gameObject con
        }

        isHit = false;

        for (int i = 0; i < 40; i++)
        {
            //Tạo phi tiêu đỏ
            Dart dart = Instantiate(redDartPrefab, transform);
            dart.gameObject.SetActive(false);
            darts.Push(dart);

            //Tạo phi tiêu xanh
            dart = Instantiate(greenDartPrefab, transform);
            dart.gameObject.SetActive(false);
            darts.Push(dart);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (currentDart != null)
        {
            if (currentDart.GetCurrentState() == DartState.Hit && !isHit)
            {
                isHit = true;
                //Hủy phi tiêu đã cắm sau 5 giây
                StartCoroutine(DisableDart(currentDart));

                //Invoke(nameof(ReloadDart), 2);
            }
        }
    }

    IEnumerator DisableDart(Dart dart)
    {
        Dart dartToDestroy = dart;
        yield return new WaitForSeconds(timeToDestroyHitDart);
        if (dartToDestroy != null)
        {
            dartToDestroy.gameObject.SetActive(false);
        }
    }

    public Dart GetCurrentDart()
    {
        return currentDart;
    }

    public Dart ReloadDart()
    {
        currentDart = darts.Pop();
        currentDart.gameObject.SetActive(true);

        CameraManager.Instance.SetCameraFollow(currentDart.gameObject);
        playerController.SetDart(currentDart);

        isHit = false;

        return currentDart;
    }
}
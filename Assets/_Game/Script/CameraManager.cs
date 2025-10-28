using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CameraType
{
    login,
    mainMenu,
    onlineListMenu,
    gameplay,
    ranking
}

public class CameraManager : Singleton<CameraManager>
{
    //[SerializeField] GameObject target;
    //[SerializeField] float moveSpeed;
    //[SerializeField] Vector3 offset;

    [SerializeField] CinemachineVirtualCamera loginCamera;
    [SerializeField] CinemachineVirtualCamera mainMenuCamera;
    [SerializeField] CinemachineVirtualCamera onlineListMenuCamera;
    [SerializeField] CinemachineVirtualCamera gamePlayCamera;
    [SerializeField] CinemachineVirtualCamera rankingCamera;

    private CinemachineVirtualCamera currentCVCamera;

    [Header("DEBUG")]
    //DEBUG
    [SerializeField] private CinemachineFramingTransposer currentTransposer;
    //transposer
    [SerializeField] private CinemachineFramingTransposer loginTransposer;
    [SerializeField] private CinemachineFramingTransposer mainMenuTransposer;
    [SerializeField] private CinemachineFramingTransposer onlineListMenuTransposer;
    [SerializeField] private CinemachineFramingTransposer gamePlayTransposer;
    [SerializeField] private CinemachineFramingTransposer rankingTransposer;

    private Dictionary<CinemachineVirtualCamera, CinemachineFramingTransposer> cameraMap = new Dictionary<CinemachineVirtualCamera, CinemachineFramingTransposer>();
    void Start()
    {
        loginTransposer = loginCamera.GetCinemachineComponent<CinemachineFramingTransposer>();
        mainMenuTransposer = mainMenuCamera.GetCinemachineComponent<CinemachineFramingTransposer>();
        onlineListMenuTransposer = onlineListMenuCamera.GetCinemachineComponent<CinemachineFramingTransposer>();
        gamePlayTransposer = gamePlayCamera.GetCinemachineComponent<CinemachineFramingTransposer>();
        rankingTransposer = rankingCamera.GetCinemachineComponent<CinemachineFramingTransposer>();

        cameraMap.Add(loginCamera, loginTransposer);
        cameraMap.Add(mainMenuCamera, mainMenuTransposer);
        cameraMap.Add(onlineListMenuCamera, onlineListMenuTransposer);
        cameraMap.Add(gamePlayCamera, gamePlayTransposer);
        cameraMap.Add(rankingCamera, rankingTransposer);

        SetCamera(CameraType.login);
    }

    //void FixedUpdate()
    //{
    //    transform.position = Vector3.Lerp(transform.position, target.transform.position + offset, moveSpeed *  Time.deltaTime);
    //}

    //public void SetTarget(GameObject newTarget)
    //{
    //    target = newTarget;
    //}






    public void SetCamera(CameraType cameraType)
    {
        CinemachineVirtualCamera targetCam;
        switch (cameraType)
        {
            case CameraType.login:
                targetCam = loginCamera; break;
            case CameraType.mainMenu:
                targetCam = mainMenuCamera; break;
            case CameraType.onlineListMenu:
                targetCam = onlineListMenuCamera; break;
            case CameraType.gameplay:
                targetCam = gamePlayCamera; break;
            case CameraType.ranking:
                targetCam = rankingCamera; break;
            default:
                Debug.Log("Không có camera hợp lệ");
                return;
        }

        
        foreach (var pair in cameraMap)
        {
            if (pair.Key == targetCam)
            {
                targetCam.gameObject.SetActive(true);
                currentCVCamera = targetCam;
                currentTransposer = pair.Value;
            }
            else
            {
                pair.Key.gameObject.SetActive(false);
            }
        }
    }

    public void SetCameraFollow(GameObject follow)
    {
        currentCVCamera.Follow = follow.transform;
    }

}

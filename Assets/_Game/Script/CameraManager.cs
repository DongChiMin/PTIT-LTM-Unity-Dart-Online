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
    [SerializeField] GameObject target;
    [SerializeField] float moveSpeed;
    [SerializeField] Vector3 offset;



    void FixedUpdate()
    {
        if (target != null) { 
            transform.position = Vector3.Lerp(transform.position, target.transform.position + offset, moveSpeed * Time.deltaTime);
        }
    }

    public void SetTarget(GameObject newTarget)
    {
        target = newTarget;
    }






    public void SetCamera(CameraType cameraType)
    {
        
      
    }

    public void SetCameraFollow(GameObject follow)
    {
       
    }

}

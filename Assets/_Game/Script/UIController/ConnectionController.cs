using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConnectionController : MonoBehaviour
{
    public void OnClickTryAgain()
    {
        ServerConnection.Instance.StartConnect();
    }
}

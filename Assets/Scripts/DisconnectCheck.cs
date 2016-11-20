using UnityEngine;
using System.Collections;

public class ExampleClass : MonoBehaviour
{
    void OnDisconnectedFromServer(NetworkDisconnection info)
    {
        if (Network.isServer)
            Debug.Log("Local server connection disconnected");
        else
            if (info == NetworkDisconnection.LostConnection)
            Debug.Log("Lost connection to the server");
        else
            Debug.Log("Successfully diconnected from the server");
    }
}
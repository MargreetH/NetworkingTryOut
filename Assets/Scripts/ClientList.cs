using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class ClientList : NetworkBehaviour
{
    private int numberOfClients;

    public override void OnStartServer()
    {
        numberOfClients = Network.connections.Length;
    }


    [ServerCallback]
	void Update () {
	    
        if (numberOfClients < fetchNumberOfClients())
        {
            numberOfClients = fetchNumberOfClients();
            RpcclientAdded();
        } else if (numberOfClients < fetchNumberOfClients())
        {
            numberOfClients = fetchNumberOfClients();
            RpcclientRemoved();
        } else
        {

        }
	}

    [ClientRpc]
    void RpcclientAdded()
    {
        Debug.Log("Client added");
    }

    [ClientRpc]
    void RpcclientRemoved()
    {
        Debug.Log("Client removed");
    }

    private int fetchNumberOfClients()
    {
        if (Network.connections != null)
        {
            return Network.connections.Length;
        }
        return 0;
    }

}

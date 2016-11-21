using UnityEngine;
using System.Collections;

public class HudScript : MonoBehaviour {

    private PlayerController[] players;
    private PlayerController playerSelf;
    private int numberOfCurrentPlayers;

	// Use this for initialization
	void Start () {

	 if (!playersIngame())
        {
            return;
        }


	}
	
	// Update is called once per frame
	void Update () {
	
	}

    private void addPlayer()
    {

    }

    private void removePlayer()
    {

    }

    //Check if there are players ingame
    private bool playersIngame()
    {
        int numberOfPlayers = GameObject.FindGameObjectsWithTag("Player").Length + GameObject.FindGameObjectsWithTag("PlayerSelf").Length;

        if (numberOfPlayers == 0)
        {
            return false;
        }
        return true;
    }
}

using UnityEngine;
using System.Collections;

public class HudScript : MonoBehaviour {

    private PlayerController[] players;
    private PlayerController playerSelf;
    private int numberOfCurrentPlayers;
    public GameObject scoreTextPrefab;
    bool selfAdded = false;

    private GameObject selfScoreBar;

    // Use this for initialization
    void Start () {

	 if (!playersIngame())
        {
            return;
        }

        addSelf();




	}
	
	// Update is called once per frame
	void Update () {
        if (!selfAdded)
        {
            selfAdded = true;
            addSelf();
        }
	
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

    private void addSelf()
    {
        selfScoreBar = (GameObject)Instantiate(scoreTextPrefab) as GameObject;
        selfScoreBar.transform.SetParent(gameObject.transform, false);

        Vector3 position1 = new Vector3(0f, 0f, 0f);
        position1.x += gameObject.transform.position.x - 100f;
        position1.y += gameObject.transform.position.y -50f;
        selfScoreBar.transform.localPosition = position1;     
    }
}
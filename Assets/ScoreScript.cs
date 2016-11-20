using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScoreScript : MonoBehaviour {

    Text scoreText;
    PlayerController attachedEntity;
    private bool hadPreviousEntity = false;

	// Use this for initialization
	void Start () {
        scoreText = gameObject.GetComponent<Text>();
    }

    void setAttachedEntity(PlayerController entity)
    {
        this.attachedEntity = entity;
        hadPreviousEntity = true;
    }
	
	// Update is called once per frame
	void Update () {

        if (attachedEntity == null)
        {
            setAttachedPlayerSelf();
            if (attachedEntity == null)
            {
                return;
            }
        }
        scoreText.text = "Player 1: " + attachedEntity.score;
        
    }

    public void setAttachedPlayerSelf()
    {
        GameObject[] player = GameObject.FindGameObjectsWithTag("PlayerSelf");
        if (player.Length == 0)
        {
            return;
        }
        attachedEntity = player[0].GetComponent<PlayerController>();
        hadPreviousEntity = true;
    }

    public void setAttachedPlayerOther(PlayerController other)
    {
        this.attachedEntity = other;
        hadPreviousEntity = true;
    }
}

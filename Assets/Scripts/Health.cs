using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Collections;

public class Health : NetworkBehaviour
{

    public const int maxHealth = 100;
    public bool destroyOnDeath;
    public GameObject healthBarPrefab;
    public int scoreOnKill;

    [SyncVar(hook = "OnChangeHealth")]
    public int currentHealth = maxHealth;


    public RectTransform healthBar;

    private NetworkStartPosition[] spawnPoints;

    void Start()
    {
        if (isLocalPlayer)
        {
            spawnPoints = FindObjectsOfType<NetworkStartPosition>();
        }

        //If healthbar is null, we create a new one.
        if (healthBar == null)
        {
          GameObject healthCanvas = (GameObject)Instantiate(healthBarPrefab, transform.position, Quaternion.identity);
          healthBar = healthCanvas.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.GetComponent<RectTransform>();
          healthCanvas.GetComponent<Billboard>().attachedEntity = gameObject;
        }
    }

    public void TakeDamage(int amount, GameObject firedBy, int scoreAmount)
    {
        if (!isServer)
            return;

        currentHealth -= amount;
        if (currentHealth <= 0)
        {
            firedBy.GetComponent<PlayerController>().addScore(scoreAmount);
            if (destroyOnDeath) //For the normal PvE enemies
            {

                //Also destroy the healthbar here!!!
                //Get the parent of the foreground -> the complete healthbar

                //This should give the Healthbar Canvas GO
                GameObject healthbarCanvas = healthBar.gameObject.transform.parent.gameObject.transform.parent.gameObject;

                //Destroy the healthbar and this GO
                Destroy(healthbarCanvas);
                Destroy(gameObject);           
            }
            else
            {
                currentHealth = maxHealth;

                // called on the Server, invoked on the Clients
                RpcRespawn();
            }
        }
    }

    void OnChangeHealth(int currentHealth)
    {
        healthBar.sizeDelta = new Vector2(currentHealth, healthBar.sizeDelta.y);
    }

    [ClientRpc]
    void RpcRespawn()
    {
        if (isLocalPlayer)
        {
            // Set the spawn point to origin as a default value
            Vector3 spawnPoint = Vector3.zero;

            // If there is a spawn point array and the array is not empty, pick one at random
            if (spawnPoints != null && spawnPoints.Length > 0)
            {
                spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)].transform.position;
            }

            // Set the player’s position to the chosen spawn point
            transform.position = spawnPoint;
        }
    }
}
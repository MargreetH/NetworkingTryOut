using UnityEngine;
using UnityEngine.Networking;

public class PlayerController : NetworkBehaviour
{
    public GameObject bulletPrefab;
    public Transform bulletSpawn;
    public GameObject healthBarPrefab;
    [SyncVar]
    public int score;

    void Start()
    {

        this.score = 0;
        var healthBar = (GameObject)Instantiate(healthBarPrefab, transform.position, transform.rotation);
        Billboard b = healthBar.GetComponent<Billboard>();
        b.attachedEntity = gameObject;
        Health health = GetComponent<Health>();

        //Get the health bar
        RectTransform hb = healthBar.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.GetComponent<RectTransform>();
        health.healthBar = hb;
    }

    void Update()
    {
        if (!isLocalPlayer)
        {
            return;
        }

        gameObject.tag = "PlayerSelf";

        var x = Input.GetAxis("Horizontal") * Time.deltaTime * 150.0f;
        var z = Input.GetAxis("Vertical") * Time.deltaTime * 3.0f;

        transform.Rotate(0, x, 0);
        transform.Translate(0, 0, z);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            CmdFire();
        }
    }

    // This [Command] code is called on the Client …
    // … but it is run on the Server!
    [Command]
    void CmdFire()
    {
        // Create the Bullet from the Bullet Prefab
        var bullet = (GameObject)Instantiate(
            bulletPrefab,
            bulletSpawn.position,
            bulletSpawn.rotation);

        // Add velocity to the bullet
        bullet.GetComponent<Rigidbody>().velocity = bullet.transform.forward * 6;
        bullet.GetComponent<Bullet>().setFiredBy(gameObject);

        // Spawn the bullet on the Clients
        NetworkServer.Spawn(bullet);

        // Destroy the bullet after 2 seconds
        Destroy(bullet, 2.0f);
    }

    public int getPlayerScore()
    {
        return this.score;
    }

    public void addScore(int addedScore)
    {
        this.score += addedScore;
    }

    public override void OnStartLocalPlayer()
    {
        GetComponent<MeshRenderer>().material.color = Color.blue;
    }

    public bool isLocalPlayerBoolean()
    {
        return isLocalPlayer;
    }
}
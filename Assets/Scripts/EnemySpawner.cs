using UnityEngine;
using UnityEngine.Networking;

public class EnemySpawner : NetworkBehaviour
{

    public GameObject enemyPrefab;
    public int numberOfEnemies;
    float spawnTimer;
    public float spawnTime;
    private int counter;
    private int numberOfEnemiesSpawned;
    private float timeSinceGameStarted;

    public override void OnStartServer()
    {
        counter = 0;
        spawnTimer = 0;
        spawnEnemy(numberOfEnemies);
        timeSinceGameStarted = Time.timeSinceLevelLoad;
    }

    [ServerCallback]
    void Update()
    {
        spawnTimer = Time.timeSinceLevelLoad - counter*spawnTime - timeSinceGameStarted;
        Debug.Log(spawnTimer);

        if (spawnTimer >= spawnTime)
        {
            spawnEnemy(1);
            counter++;
        }
    }

    void spawnEnemy(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            var spawnPosition = new Vector3(
                Random.Range(-8.0f, 8.0f),
                1.0f,
                Random.Range(-8.0f, 8.0f));

            var spawnRotation = Quaternion.Euler(
                0.0f,
                Random.Range(0, 180),
                0.0f);

            var enemy = (GameObject)Instantiate(enemyPrefab, spawnPosition, spawnRotation);

            numberOfEnemies++;
            NetworkServer.Spawn(enemy);
        }
    }
}
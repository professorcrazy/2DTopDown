using UnityEngine;

public class EnemySpawner : MonoBehaviour
{

    float spawnWith = 73f;
    float spawnHeight = 73;
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private Transform playerRef;
    [SerializeField] private float minSpawnDistance = 15f;
    [SerializeField] private float minSpawnDistancePlayer = 25f;
    private Vector3 lastSpwanPoint = Vector3.zero;
    private void Start()
    {
        if (playerRef == null)
        {
            playerRef = GameObject.FindGameObjectWithTag("Player").transform;
        }
    }
    public void SpawnEnemy()
    {
        if (enemyPrefab == null)
        {
            Debug.LogError("Enemy prefab is not assigned in the EnemySpawner script.");
            return;
        }
        if (playerRef == null)
        {
            Debug.LogError("Player reference is not assigned in the EnemySpawner script.");
            return;
        }
        Vector3 spawnPosition = new Vector3(Random.Range(-spawnWith, spawnWith), Random.Range(-spawnHeight, spawnHeight), 0f);
        while (Vector3.Distance(spawnPosition, playerRef.position) < minSpawnDistancePlayer && Vector3.Distance(spawnPosition, lastSpwanPoint) < minSpawnDistance)
        {
            spawnPosition = new Vector3(Random.Range(-spawnWith, spawnWith), Random.Range(-spawnHeight, spawnHeight), 0f);
        }
        lastSpwanPoint = spawnPosition;
        Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
    }

    public void SpawnEnemy(GameObject enemeyVersionPrefab)
    {
        if (enemyPrefab == null)
        {
            Debug.LogError("Enemy prefab is not assigned in the EnemySpawner script.");
            return;
        }
        if (playerRef == null)
        {
            Debug.LogError("Player reference is not assigned in the EnemySpawner script.");
            return;
        }
        Vector3 spawnPosition = new Vector3(Random.Range(-spawnWith, spawnWith), Random.Range(-spawnHeight, spawnHeight), 0f);
        while (Vector3.Distance(spawnPosition, playerRef.position) < minSpawnDistancePlayer && Vector3.Distance(spawnPosition, lastSpwanPoint) < minSpawnDistance)
        {
            spawnPosition = new Vector3(Random.Range(-spawnWith, spawnWith), Random.Range(-spawnHeight, spawnHeight), 0f);
        }
        lastSpwanPoint = spawnPosition;
        Instantiate(enemeyVersionPrefab, spawnPosition, Quaternion.identity);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(transform.position, new Vector3(spawnWith*2, spawnHeight*2, 0.1f));
    }

}

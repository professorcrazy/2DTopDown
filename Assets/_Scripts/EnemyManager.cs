using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager Instance;
    [SerializeField] private EnemySpawner[] enemyPrefabs;
    private void Start()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        Instance = this;
    }

    public void SpawnEnemy()
    {
        int enemyIndex = Random.Range(0, enemyPrefabs.Length);
        if (enemyPrefabs == null || enemyPrefabs[enemyIndex] == null)
        {
            Debug.LogError("Enemy prefab is not assigned or index is out of range in the EnemyManager script.");
            return;
        }
        while(enemyPrefabs.Length >= enemyIndex)
        {
            enemyIndex = Random.Range(0, enemyPrefabs.Length);

        }
        enemyPrefabs[enemyIndex].SpawnEnemy();
    }
    public void SpawnEnemy(int weight)
    {
        Debug.Log("Spawning Enemy with weight: " + weight);
        int enemyIndex = Random.Range(0, enemyPrefabs.Length) + weight;
        enemyIndex = Mathf.Clamp(enemyIndex, 0, enemyPrefabs.Length - 1);

        Debug.Log("Calculated enemy index: " + enemyIndex);
        if (enemyPrefabs == null || enemyPrefabs[enemyIndex] == null)
        {
            Debug.LogError("Enemy prefab is not assigned or index is out of range in the EnemyManager script.");
            return;
        }
        int maxTys = 4;
        while (enemyPrefabs.Length <= enemyIndex)
        {
            maxTys--;
            if (maxTys <= 0)
            {
                Debug.LogError("Enemy index is out of range and max retries reached. Cannot spawn enemy.");
                return;
            }
            Debug.Log("Enemy index out of range, recalculating...");

            enemyIndex = Random.Range(0, enemyPrefabs.Length) + weight;
            enemyIndex = Mathf.Clamp(enemyIndex, 0, enemyPrefabs.Length - 1);

        }
        enemyPrefabs[enemyIndex].SpawnEnemy();
    }
}

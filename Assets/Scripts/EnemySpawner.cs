using UnityEngine;

public class EnemySpawner : MonoBehaviour
{

    public Transform[] spawnPoints;
    public GameObject enemyPrefab;

    private bool[] isOccupied;

    private void Start()
    {
        isOccupied = new bool[spawnPoints.Length];
    }

    private void Update()
    {
        SpawnEnemy();
    }

    private void SpawnEnemy()
    {
        for (int i = 0; i < spawnPoints.Length; i++)
        {
            if (!isOccupied[i])
            {
                GameObject enemy = Instantiate(enemyPrefab, spawnPoints[i].position, Quaternion.identity);
                enemy.GetComponent<Enemy>().spawnIndex = i;
                enemy.GetComponent<Enemy>().spawner = this;
                isOccupied[i] = true;
                break;
            }
        }
    }

    public void FreeSpawnPoint(int index)
    {
        isOccupied[index] = false;
    }
}

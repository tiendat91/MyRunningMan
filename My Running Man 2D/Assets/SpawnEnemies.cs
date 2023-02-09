using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class SpawnEnemies : MonoBehaviour
{
    public GameObject enemyPrefab;
    public float spawnTime = 2f;
    public Tilemap tilemap;
    private Queue<GameObject> enemyQueue = new Queue<GameObject>();

    void Start()
    {
        // Adding enemy prefabs to the queue
        enemyQueue.Enqueue(enemyPrefab);
        enemyQueue.Enqueue(enemyPrefab);
        enemyQueue.Enqueue(enemyPrefab);

        // Invoking the spawn method every spawnTime seconds
        InvokeRepeating("SpawnEnemy", spawnTime, spawnTime);
    }

    void SpawnEnemy()
    {
        // Checking if the queue is not empty
        if (enemyQueue.Count > 0)
        {
            // Removing the first enemy prefab from the queue
            GameObject enemy = enemyQueue.Dequeue();

            // Instantiating the enemy at a random position on the x axis
            Vector3 spawnPosition = tilemap.GetCellCenterWorld(new Vector3Int(Random.Range(0, tilemap.size.x), Random.Range(0, tilemap.size.y), 0));
            Instantiate(enemy, spawnPosition, Quaternion.identity, transform);
        }
    }
}

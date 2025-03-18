using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public Transform spawnPoint;
    public float spawnTimer = 5f;
    private int spawnCount = 0;
    public float maxSpawn = 10;
    // Start is called before the first frame update
    void Start()
    {

        StartCoroutine(SpawnEnemies());


    }
    private IEnumerator SpawnEnemies()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnTimer);

            if (spawnCount >= maxSpawn)
            {
                yield break;
            }
            SpawnEnemy();
        }

    }

    private void SpawnEnemy()
    {
        Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);
        spawnCount++;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] GameObject enemyPrefab;
    [SerializeField] Transform enemySpawnPosition;
    [SerializeField] int enemyCount;
    [SerializeField] int spawnedEnemyLimit;
    [SerializeField] float spawnTimer = 10;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //Create countdown;
        {
            InstantiateEnemy();
        }
    }

    void InstantiateEnemy()
    {
        Instantiate(enemyPrefab, enemySpawnPosition);
        enemyCount++;
    }
}

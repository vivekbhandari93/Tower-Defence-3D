using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] float spawnDelay = 10f;
    [SerializeField] EnemyDamage enemyPrefab;

    [SerializeField] Transform enemiesTransform;

    void Start()
    {
        StartCoroutine(SpawnEnemy());
    }

    IEnumerator SpawnEnemy()
    {
        while (true)
        {
            var enemyInstance = Instantiate(enemyPrefab, transform.position, Quaternion.identity);
            enemyInstance.transform.parent = enemiesTransform;

            yield return new WaitForSeconds(spawnDelay);
        }
    }
}

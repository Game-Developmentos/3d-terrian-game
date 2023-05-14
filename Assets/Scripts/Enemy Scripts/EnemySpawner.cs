using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemySpawner : MonoBehaviour
{
    public GameObject[] zombiePrefabs;
    [SerializeField] private float minTimeToSpawn = 5f;
    [SerializeField] private float maxTimeToSpawn = 15f;

    private int numOfEnemies;

    private float SpawnTime;
    void Start()
    {
        numOfEnemies = 0;
        ScheduleNextSpawn();
    }

    private void ScheduleNextSpawn()
    {
        SpawnTime = Random.Range(minTimeToSpawn, maxTimeToSpawn);
        Invoke("spawnEnemy", SpawnTime);
    }
    void spawnEnemy()
    {
        NavMeshHit hit;
        Vector3 randomPos = transform.position + Random.insideUnitSphere * 10f;
        if (NavMesh.SamplePosition(randomPos, out hit, 10f, NavMesh.AllAreas) && numOfEnemies < 20)
        {
            int randomIndex = Random.Range(0, zombiePrefabs.Length);
            Instantiate(zombiePrefabs[randomIndex], hit.position, Quaternion.identity);
            numOfEnemies += 1;
        }
        ScheduleNextSpawn();

    }
}

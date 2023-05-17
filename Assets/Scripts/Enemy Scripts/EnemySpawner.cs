using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemySpawner : MonoBehaviour
{
    public GameObject[] zombiePrefabs;
    [SerializeField] private float minTimeToSpawn = 4f;
    [SerializeField] private float maxTimeToSpawn = 8f;
    private int numOfEnemies;
    private float overlapRadius = 0.1f;
    private float SpawnTime;
    private float SamplePositionMaxDist = 10f;

    private float spawnRadius = 25f;
    [SerializeField] private int maxEnemiesPerLocation = 20;
    private void Start()
    {
        numOfEnemies = 0;
        ScheduleNextSpawn();
    }

    private bool HasCollision(Vector3 currPos)
    {
        int maxColliders = 5;
        Collider[] hitColliders = new Collider[maxColliders];
        int numColliders = Physics.OverlapSphereNonAlloc(currPos, overlapRadius, hitColliders);
        return numColliders > 0;
    }
    private void ScheduleNextSpawn()
    {
        SpawnTime = Random.Range(minTimeToSpawn, maxTimeToSpawn);
        Invoke("SpawnEnemy", SpawnTime);
    }
    private void SpawnEnemy()
    {
        NavMeshHit hit;
        Vector3 randomPos = transform.position + Random.insideUnitSphere * spawnRadius;
        if (HasCollision(randomPos))
        {
            ScheduleNextSpawn();
            return;
        }
        if (NavMesh.SamplePosition(randomPos, out hit, SamplePositionMaxDist, NavMesh.AllAreas) && numOfEnemies < maxEnemiesPerLocation)
        {
            int randomIndex = Random.Range(0, zombiePrefabs.Length);
            GameObject enemy = Instantiate(zombiePrefabs[randomIndex], hit.position, Quaternion.identity);
            enemy.gameObject.SetActive(true);
            numOfEnemies += 1;
        }
        ScheduleNextSpawn();
    }
}

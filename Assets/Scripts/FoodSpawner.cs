using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FoodSpawner : MonoBehaviour
{
    public GameObject[] FoodPrefabs;
    [SerializeField] private float minTimeToSpawn = 5f;
    [SerializeField] private float maxTimeToSpawn = 15f;
    private int numOfObjectsSpawned;
    private int maxObjectsToSpawn = 20;
    private float SpawnTime;
    private float spawnRadius = 25f;
    private float overlapRadius = 0.1f;
    private float SamplePositionMaxDist = 10f;


    // Starts the food spawning process when the component is enabled.
    private void Start()
    {
        numOfObjectsSpawned = 0;
        ScheduleNextSpawn();
    }

    // Schedules the next food spawn based on the defined time range.
    private void ScheduleNextSpawn()
    {
        SpawnTime = Random.Range(minTimeToSpawn, maxTimeToSpawn);
        Invoke("SpawnFood", SpawnTime);
    }

    // Checks if there is any collision at the specified position.
    private bool HasCollision(Vector3 currPos)
    {
        int maxColliders = 5;
        Collider[] hitColliders = new Collider[maxColliders];
        int numColliders = Physics.OverlapSphereNonAlloc(currPos, overlapRadius, hitColliders);
        return numColliders > 0;
    }

    // Spawns a food object at a random position within the spawn radius.
    void SpawnFood()
    {
        NavMeshHit hit;
        Vector3 randomPos = transform.position + Random.insideUnitSphere * spawnRadius;
        if (HasCollision(randomPos))
        {
            ScheduleNextSpawn();
            return;
        }
        if (NavMesh.SamplePosition(randomPos, out hit, SamplePositionMaxDist, NavMesh.AllAreas) && numOfObjectsSpawned < maxObjectsToSpawn)
        {
            int randomIndex = Random.Range(0, FoodPrefabs.Length);
            Instantiate(FoodPrefabs[randomIndex], hit.position, Quaternion.identity);
            numOfObjectsSpawned += 1;
        }
        ScheduleNextSpawn();
    }

}

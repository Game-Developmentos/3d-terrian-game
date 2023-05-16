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

    private float SpawnTime;


    private void Start()
    {
        numOfObjectsSpawned = 0;
        ScheduleNextSpawn();
    }

    private void ScheduleNextSpawn()
    {
        SpawnTime = Random.Range(minTimeToSpawn, maxTimeToSpawn);
        Invoke("SpawnFood", SpawnTime);
    }

    private bool HasCollision(Vector3 currPos)
    {
        int maxColliders = 5;
        Collider[] hitColliders = new Collider[maxColliders];
        int numColliders = Physics.OverlapSphereNonAlloc(currPos, 0.1f, hitColliders);
        return numColliders > 0;
    }
    void SpawnFood()
    {
        NavMeshHit hit;
        Vector3 randomPos = transform.position + Random.insideUnitSphere * 25f;
        if (HasCollision(randomPos))
        {
            ScheduleNextSpawn();
            return;
        }
        if (NavMesh.SamplePosition(randomPos, out hit, 10f, NavMesh.AllAreas) && numOfObjectsSpawned < 20)
        {
            int randomIndex = Random.Range(0, FoodPrefabs.Length);
            Instantiate(FoodPrefabs[randomIndex], hit.position, Quaternion.identity);
            numOfObjectsSpawned += 1;
        }
        ScheduleNextSpawn();
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemySpawner : MonoBehaviour
{
    public GameObject[] zombiePrefabs;

    [SerializeField] private float spawnInterval = 5f;
    void Start()
    {
        InvokeRepeating("spawnEnemy", spawnInterval, spawnInterval);

    }

    void spawnEnemy()
    {
        NavMeshHit hit;
        Vector3 randomPos = transform.position + Random.insideUnitSphere * 10f;
        if (NavMesh.SamplePosition(randomPos, out hit, 10f, NavMesh.AllAreas))
        {
            int randomIndex = Random.Range(0, zombiePrefabs.Length);
            Instantiate(zombiePrefabs[randomIndex], hit.position, Quaternion.identity);
        }
    }


}

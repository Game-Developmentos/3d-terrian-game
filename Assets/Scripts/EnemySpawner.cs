using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject zombiePrefab;

    [SerializeField] private float spawnInterval = 5f;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("spawnEnemy", spawnInterval, spawnInterval);

    }

    void spawnEnemy()
    {
        Vector3 randomPos = transform.position + Random.insideUnitSphere * 10f;
        Instantiate(zombiePrefab, randomPos, Quaternion.identity);


    }

    // Update is called once per frame
    void Update()
    {

    }
}

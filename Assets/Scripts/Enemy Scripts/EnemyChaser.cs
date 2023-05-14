using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyChaser : MonoBehaviour
{
    [Tooltip("The object that this enemy chases after")]
    [SerializeField] GameObject player;
    private NavMeshAgent navMeshAgent;
    [SerializeField] private Vector3 playerPosition;


    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        playerPosition = player.transform.position;
        float distanceToPlayer = Vector3.Distance(playerPosition, transform.position);
        // FacePlayer();
        if (navMeshAgent.isActiveAndEnabled)
        {
            navMeshAgent.SetDestination(playerPosition);

        }

    }
    internal Vector3 GetTargetPosition()
    {
        return player.transform.position;
    }

}

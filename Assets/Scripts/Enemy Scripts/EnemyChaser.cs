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
    // private void FacePlayer()
    // {
    //     Vector3 direction = (playerPosition - transform.position).normalized;
    //     Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
    //     transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5);
    // }

    internal Vector3 GetTargetPosition()
    {
        return player.transform.position;
    }

}

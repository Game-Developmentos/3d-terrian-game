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

    // Updates the position of the player object and sets it as the destination for the navMeshAgent if active.
    private void Update()
    {
        playerPosition = player.transform.position;
        if (navMeshAgent.isActiveAndEnabled)
        {
            navMeshAgent.SetDestination(playerPosition);
        }

    }
    // Retrieves the position of the player object.
    public Vector3 GetTargetPosition()
    {
        return player.transform.position;
    }

}

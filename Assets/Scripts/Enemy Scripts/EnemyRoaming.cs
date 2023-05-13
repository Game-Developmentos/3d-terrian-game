using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyRoaming : MonoBehaviour
{
    private Vector3 startingPosition;
    private Vector3 roamPosition;
    private NavMeshAgent navMeshAgent;
    private Animator animator;
    private float waitTimeBeforePatrol = 2f;
    private int isWalkingHash;

    private float maxTimer = 2f;
    private float currTimer = 2f;

    private int interactLayer = 6;
    private void Awake()
    {
        isWalkingHash = Animator.StringToHash("isWalking");
    }
    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        startingPosition = transform.position;
        roamPosition = GetRoamingPosition();

    }
    private Vector3 GetRandomDirection()
    {
        return new Vector3(Random.Range(-1f, 1f), 0, Random.Range(-1f, 1f)).normalized;
    }

    private Vector3 GetRoamingPosition()
    {
        return startingPosition + GetRandomDirection() * Random.Range(5f, 10f);
    }

    bool isOverLappingAnotherEnemy()
    {
        int interactLayerMask = 1 << interactLayer;
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, 0.1f, interactLayerMask);
        return hitColliders.Length > 0;
    }

    void SetNewDestination()
    {
        roamPosition = GetRoamingPosition();
        NavMeshHit hit;
        if (NavMesh.SamplePosition(roamPosition, out hit, 1f, NavMesh.AllAreas))
        {
            navMeshAgent.SetDestination(hit.position);
        }
    }
    void Update()
    {
        if (waitTimeBeforePatrol >= 0)
        {
            waitTimeBeforePatrol -= Time.deltaTime;
            return;
        }
        float enemySpeed = navMeshAgent.velocity.magnitude;
        animator.SetFloat("Speed", enemySpeed * 4);
        animator.SetBool(isWalkingHash, true);

        if (isOverLappingAnotherEnemy())
        {
            roamPosition = GetRoamingPosition();
            if (currTimer > 0)
            {
                currTimer -= Time.deltaTime;
            }
            else
            {
                navMeshAgent.SetDestination(roamPosition);
                currTimer = maxTimer;

            }
        }


        if (!navMeshAgent.pathPending && !navMeshAgent.hasPath)
        {
            SetNewDestination();
        }
    }
}

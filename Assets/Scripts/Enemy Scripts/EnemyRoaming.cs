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
    private float changeDirectionDelay = 2f;
    private int interactLayer = 6;
    private float overlapRadius = 0.1f;

    private float SamplePositionMaxDist = 1f;
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
        float randomDirectionX = Random.Range(-1f, 1f);
        float randomDirectionZ = Random.Range(-1f, 1f);
        return new Vector3(randomDirectionX, 0, randomDirectionZ).normalized;
    }

    private Vector3 GetRoamingPosition()
    {
        float distance = Random.Range(5f, 10f);
        return startingPosition + GetRandomDirection() * distance;
    }

    bool isOverLappingAnotherEnemy()
    {
        int interactLayerMask = 1 << interactLayer;
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, overlapRadius, interactLayerMask);
        return hitColliders.Length > 0;
    }

    void SetNewDestination()
    {
        roamPosition = GetRoamingPosition();
        NavMeshHit hit;
        if (NavMesh.SamplePosition(roamPosition, out hit, SamplePositionMaxDist, NavMesh.AllAreas))
        {
            navMeshAgent.SetDestination(hit.position);
        }
    }
    void Update()
    {
        bool isReadyToRoam = waitTimeBeforePatrol < 0;
        if (!isReadyToRoam)
        {
            waitTimeBeforePatrol -= Time.deltaTime;
            return;
        }

        float enemySpeed = navMeshAgent.velocity.magnitude;
        animator.SetBool(isWalkingHash, true);

        if (isOverLappingAnotherEnemy())
        {
            roamPosition = GetRoamingPosition();
            bool isReadyForNewPosition = changeDirectionDelay < 0;
            if (!isReadyForNewPosition)
            {
                changeDirectionDelay -= Time.deltaTime;
            }
            else
            {
                navMeshAgent.SetDestination(roamPosition);
                changeDirectionDelay = maxTimer;

            }
        }

        if (!navMeshAgent.pathPending && !navMeshAgent.hasPath)
        {
            SetNewDestination();
        }
    }
}

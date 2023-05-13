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
    private float timer = 3f;
    private int isWalkingHash;
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
    void Update()
    {
        if (timer >= 0)
        {
            timer -= Time.deltaTime;
            return;
        }
        float enemySpeed = navMeshAgent.velocity.magnitude;
        animator.SetFloat("Speed", enemySpeed * 4);
        animator.SetBool(isWalkingHash, true);
        navMeshAgent.SetDestination(roamPosition);
        float reachedPositionDistance = 1f;
        if (Vector3.Distance(transform.position, roamPosition) < reachedPositionDistance)
        {
            roamPosition = GetRoamingPosition();
        }
    }
}

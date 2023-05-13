using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(EnemyChaser))]
[RequireComponent(typeof(EnemyRoaming))]
public class EnemyControllerStateMachine : StateMachine
{

    [SerializeField] float dangerRadius = 3f;
    private EnemyChaser chaser;
    private EnemyRoaming roaming;

    private void Awake()
    {
        chaser = GetComponent<EnemyChaser>();
        roaming = GetComponent<EnemyRoaming>();
        base.AddState(roaming) // first active state
        .AddState(chaser)
        .AddTransition(roaming, () => DistanceToTarget() <= dangerRadius, chaser)
        .AddTransition(chaser, () => DistanceToTarget() > dangerRadius, roaming);
    }

    private float DistanceToTarget()
    {
        return Vector3.Distance(transform.position, chaser.GetTargetPosition());
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, dangerRadius);
    }

}

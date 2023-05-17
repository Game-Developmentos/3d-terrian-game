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

    private Animator animator;

    private int isWalkingHash;
    private int isAttackingHash;


    private void Awake()
    {
        animator = GetComponent<Animator>();
        isWalkingHash = Animator.StringToHash("isWalking");
        isAttackingHash = Animator.StringToHash("isAttacking");
        chaser = GetComponent<EnemyChaser>();
        roaming = GetComponent<EnemyRoaming>();
        // Adds the roaming and chaser states to the state machine and defines transitions between them based on the distance to the target.
        base.AddState(roaming) // first active state
        .AddState(chaser)
        .AddTransition(roaming, () => DistanceToTarget() <= dangerRadius, chaser)
        .AddTransition(chaser, () => DistanceToTarget() > dangerRadius, roaming);
    }

    // Calculates the distance between the enemy and its target (player).
    private float DistanceToTarget()
    {
        return Vector3.Distance(transform.position, chaser.GetTargetPosition());
    }

    // Draws a wire sphere in the editor to visualize the danger radius.
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, dangerRadius);
    }

    // Handles collision with the player when entering.
    private void OnCollisionEnter(Collision other)
    {
        if (gameObject && other.gameObject.CompareTag("Player"))
        {
            animator.SetBool(isWalkingHash, false);
            animator.SetBool(isAttackingHash, true);

        }
    }

    // Handles collision with the player when exiting.
    private void OnCollisionExit(Collision other)
    {
        if (gameObject && other.gameObject.CompareTag("Player"))
        {
            animator.SetBool(isAttackingHash, false);
            animator.SetBool(isWalkingHash, true);


        }
    }


}

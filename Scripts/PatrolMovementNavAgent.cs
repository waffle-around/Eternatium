using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PatrolMovementNavAgent : MonoBehaviour
{
    NavMeshAgent navMeshAgent;
    public Transform patrolPath;
    public float minimumReachDistance = 0.3f;
    public float moveSpeed = 1;
    public float rotationSpeed = 1;
    int targetIndex = 0;
    Transform target;
    Animator animator;

    private void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        target = patrolPath.GetChild(targetIndex);
        animator = GetComponent<Animator>();
        animator.SetBool("Walk Forward", true);
    }

    public void UpdateMovement()
    {
        navMeshAgent.SetDestination(target.position);

        if (Vector3.Distance(transform.position, target.position) < minimumReachDistance)
        {
            targetIndex++;
            if (targetIndex >= patrolPath.childCount) targetIndex = 0;
            target = patrolPath.GetChild(targetIndex);
        }
    }
}

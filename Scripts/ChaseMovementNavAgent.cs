using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ChaseMovementNavAgent : MonoBehaviour
{
    NavMeshAgent navMeshAgent;
    Transform target;
    public float moveSpeed = 1;
    public string targetTag = "Player";
    public float rotationSpeed = 1.5f;


    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        target = GameObject.FindGameObjectWithTag(targetTag).transform;
    }

    public void UpdateMovement()
    {
        if (target.gameObject == null) return;
        transform.position = Vector3.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);
        navMeshAgent.SetDestination(target.position);
    }
}

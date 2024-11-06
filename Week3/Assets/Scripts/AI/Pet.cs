using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.AI;

public class Pet : MonoBehaviour
{
    private NavMeshAgent agent;

    [Header("Movement")]
    public float speed;

    [Header("AI")]
    private AIState nowState;
    public float destinationDistance;
    public float startFallowDistance;
    private float toPlayerDistance;
    public Transform fallowTarget;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        toPlayerDistance = Vector3.Distance(fallowTarget.position, transform.position);

        switch(nowState)
        {
            case AIState.Idle:
                PassiveUpdate();
                break;
            case AIState.Fallowing:
                FallowUpdate();
                break;
        }
    }

    private void SetState(AIState state)
    {
        nowState = state;
        
        switch (state)
        {
            case AIState.Idle:
                agent.isStopped = true;
                break;
            case AIState.Fallowing:
                agent.isStopped = false;
                break;
        }
    }

    private void PassiveUpdate()
    {
        if(toPlayerDistance > startFallowDistance)
        {
            SetState(AIState.Fallowing);
            agent.SetDestination(SearchFallowingPath());
        }
    }

    private void FallowUpdate()
    {
        if(toPlayerDistance < destinationDistance)
        {
            SetState(AIState.Idle);
            return;
        }

        agent.SetDestination(SearchFallowingPath());
    }

    private Vector3 SearchFallowingPath()
    {
        NavMeshHit hit;

        NavMesh.SamplePosition(fallowTarget.position, out hit, destinationDistance, NavMesh.AllAreas);

        return hit.position;
    }
}

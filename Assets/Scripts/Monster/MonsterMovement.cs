using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MonsterMovement : MonoBehaviour
{
    private NavMeshAgent agent;

    public float walkSpeed = 2f;
    public float runSpeed = 5f;

    private float eps = 1.5f;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    public bool IsReached(Vector3 position)
    {
        return Vector3.Distance(position, transform.position) < eps;
    }

    public void MoveTo(Vector3 targetPosition)
    {
        var canMove = NavMesh.SamplePosition(targetPosition, out var hitPoint, 20f, NavMesh.AllAreas);
        if (canMove)
        {
            agent.SetDestination(hitPoint.position);
        }
    }

    public void Run()
    {
        agent.isStopped = false;
        agent.speed = runSpeed;
    }

    public void Walk()
    {
        agent.isStopped = false;
        agent.speed = walkSpeed;
    }

    public void Stop()
    {
        agent.velocity = Vector3.zero;
        agent.isStopped = true;
    }
}

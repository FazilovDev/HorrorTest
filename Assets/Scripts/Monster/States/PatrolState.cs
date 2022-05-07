using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PatrolState : MonsterState
{
    [SerializeField] private float radiusForPlayer = 10f;
    [SerializeField] private float radiusMax = 25f;
    private Vector3 targetPosition;

    public override void Enter()
    {
        base.Enter();
        Debug.Log("patrol state enter");
        var closestPlayer = AI.FindClosestPlayer(out var distance);

        targetPosition = Random.insideUnitSphere * 25f + closestPlayer.transform.position;
        targetPosition.y = closestPlayer.transform.position.y;

        var canMove = NavMesh.SamplePosition(targetPosition, out var hit, 25f, NavMesh.AllAreas);
        if (canMove == false)
        {
            AI.ChangeState(typeof(IdleState));
            return;
        }
        targetPosition = hit.position;

        AI.Animation.ChangeAnimation(MonsterAnimation.State.Walk);

    }

    public override void Exit()
    {
        base.Exit();
        Debug.Log("patrol state exit");
    }

    public override void Init(MonsterAI ai)
    {
        base.Init(ai);
    }

    public override void Tick()
    {
        base.Tick();

        if (AI.CanChaise())
        {
            AI.ChangeState(typeof(ChaseState));
            return;
        }

        if (AI.CanAttack())
        {
            AI.ChangeState(typeof(attackState));
            return;
        }

        if (AI.Movement.IsReached(targetPosition))
        {
            AI.ChangeState(typeof(IdleState));
            return;
        }
        AI.Movement.MoveTo(targetPosition);
    }
}

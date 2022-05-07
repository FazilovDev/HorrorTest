using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : MonsterState
{
    [SerializeField] private float timeAwait = 2;
    private float currentTime;

    public override void Init(MonsterAI ai)
    {
        base.Init(ai);
    }

    public override void Enter()
    {
        base.Enter();
        AI.Animation.ChangeAnimation(MonsterAnimation.State.Idle);
        currentTime = 0f;

        Debug.Log("Idle state enter");
    }

    public override void Exit()
    {
        base.Exit();
        Debug.Log("Idle state exit");
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

        currentTime += Time.deltaTime;
        if(currentTime >= timeAwait)
        {
            AI.ChangeState(typeof(PatrolState));
            return;
        }
    }
}

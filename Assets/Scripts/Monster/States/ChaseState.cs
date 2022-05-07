using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseState : MonsterState
{
    public override void Enter()
    {
        base.Enter();

        AI.Movement.Run();
        AI.Animation.ChangeAnimation(MonsterAnimation.State.Run);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Init(MonsterAI ai)
    {
        base.Init(ai);
    }

    public override void Tick()
    {
        base.Tick();
        if (AI.CanAttack())
        {
            AI.ChangeState(typeof(attackState));
            return;
        }

        if (!AI.CanChaise())
        {
            AI.ChangeState(typeof(IdleState));
            return;
        }

        AI.Movement.MoveTo(AI.TargetPlayer.transform.position);
    }
}

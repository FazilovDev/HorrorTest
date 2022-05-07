using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class attackState : MonsterState
{
    public float damage = 20f;
    public override void Enter()
    {
        base.Enter();

        AI.Animation.OnAttack += OnAttackHandler;

        AI.Movement.Stop();
        AI.Animation.ChangeAnimation(MonsterAnimation.State.Attack);


    }

    private void OnAttackHandler()
    {
        var target = AI.TargetPlayer;
        var playerController = target.GetComponent<PlayerController>();
        playerController.TakeDamage(damage);

        if (AI.CanChaise())
        {
            AI.ChangeState(typeof(ChaseState));
            return;
        }
        else
        {
            AI.ChangeState(typeof(IdleState));
            return;
        }
    }

    public override void Exit()
    {
        base.Exit();
        AI.Animation.OnAttack -= OnAttackHandler;
    }

    public override void Init(MonsterAI ai)
    {
        base.Init(ai);
    }

    public override void Tick()
    {
        base.Tick();

        if (!AI.CanChaise())
        {
            AI.ChangeState(typeof(IdleState));
            return;
        }
    }
}

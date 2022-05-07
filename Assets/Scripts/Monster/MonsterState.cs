using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterState : MonoBehaviour
{
    public MonsterAI AI;

    public virtual void Init(MonsterAI ai)
    {
        AI = ai;
    }

    public virtual void Enter()
    {

    }

    public virtual void Exit()
    {

    }

    public virtual void Tick()
    {
        
    }
}

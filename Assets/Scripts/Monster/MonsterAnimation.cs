using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterAnimation : MonoBehaviour
{
    public enum State
    {
        Idle = 0,
        Walk = 1,
        Run,
        Jump,
        Attack,
        Rage
    }

    public const string STATE = "state";

    private Animator animator;
    private State current;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void ChangeAnimation(State state)
    {
        current = state;
        animator.SetInteger(STATE, (int)state);
    }

    public event System.Action OnAttack = delegate { };
    public void AttackEvent()
    {
        OnAttack();
    }
}

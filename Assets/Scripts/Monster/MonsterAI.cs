using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using System.Linq;
using System;

public class MonsterAI : NetworkBehaviour
{
    [SerializeField] private List<MonsterState> states = new List<MonsterState>();
    [SerializeField] private Transform statePos;
    public MonsterState Current;

    public MonsterMovement Movement;
    public MonsterAnimation Animation;


    [SerializeField] private List<GameObject> players = new List<GameObject>();

    public GameObject TargetPlayer;

    [SerializeField] private float visionDistance = 8f;
    [SerializeField] private float angleVision = 60f;

    public float attackDistance = 4f;

    private void Start()
    {
        //Movement = GetComponent<MonsterMovement>();
       // Animation = GetComponent<MonsterAnimation>();

        if (states.Count == 0)
        {
            states = statePos.GetComponentsInChildren<MonsterState>().ToList();
        }

        foreach(var state in states)
        {
            state.Init(this);
        }

        players = GameObject.FindGameObjectsWithTag("Player").ToList();

        ChangeState(typeof(IdleState));
    }

    private void Update()
    {
        if (Current != null)
        {
            TargetPlayer = SensorPlayer();
            Current.Tick();
        }
    }

    public void ChangeState(Type newState)
    {
        if (Current != null)
        {
            Current.Exit();
        }
        Current = states.FirstOrDefault(t => t.GetType() == newState);
        Current.Enter();
    }

    public bool CanAttack()
    {
        if (TargetPlayer == null)
        {
            return false;
        }

        var targetPos = TargetPlayer.transform.position;
        targetPos.y = transform.position.y;
        if (TargetPlayer != null && Vector3.Distance(transform.position, targetPos) <= attackDistance)
        {
            var playerController = TargetPlayer.GetComponent<PlayerController>();
            return !playerController.IsHidden;
        }
        return false;
    }

    public bool CanChaise()
    {
        if (TargetPlayer != null)
        {
            var playerController = TargetPlayer.GetComponent<PlayerController>();
            return !playerController.IsHidden;
        }
        return false;
    }

    public GameObject SensorPlayer()
    {
        GameObject targetPlayer = null;
        var cosAngle = Mathf.Cos(angleVision * Mathf.Deg2Rad);

        foreach (var player in players)
        {
            var dir = (player.transform.position - transform.position).normalized;
            var projectedDir = Vector3.ProjectOnPlane(dir, Vector3.up);
            var projectedScreamerForward = Vector3.ProjectOnPlane(transform.forward, Vector3.up);
            var inView = Vector3.Dot(projectedScreamerForward, projectedDir) > cosAngle;
            if (inView)
            {
                targetPlayer = player;
                break;
            }
        }

        if (targetPlayer == null)
        {
            var currentNearPlayer = FindClosestPlayer(out var dist);
            targetPlayer = dist <= 4 ? currentNearPlayer : null;
        }

        return targetPlayer;
    }

    public GameObject FindClosestPlayer(out float distance)
    {
        var minDistance = float.MaxValue;
        GameObject targetPlayer = null;

        foreach(var player in players)
        {
            var posPlayer = player.transform.position;
            posPlayer.y = transform.position.y;
            var distanceToPlayer = Vector3.Distance(posPlayer, transform.position);
            if (distanceToPlayer < minDistance)
            {
                minDistance = distanceToPlayer;
                targetPlayer = player;
            }
        }

        distance = minDistance;
        return targetPlayer;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        GizmosExtensions.DrawWireArc(transform.position, transform.forward, angleVision * 2, visionDistance);
    }
}

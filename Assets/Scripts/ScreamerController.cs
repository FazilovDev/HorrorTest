using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreamerController : MonoBehaviour
{
    public GameObject ScreamerObject;
    public AudioClip ChaseClip;
    public bool move;
    private Transform player;
    private bool isMove;
    [SerializeField] private float speed;
    private AudioSource audioSource;
    public float MinRotation = -45f;
    public float MaxRotation = 45f;
    public bool IsRotateToMax = true;
    public float SpeedRotation = 5f;
    public bool sound;
    private bool LookAtMe;
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = ChaseClip;
        audioSource.Stop();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        isMove = false;
    }

    private void Update()
    {
        UpdateRotation();

        if (move == true)
        {
            if (!isMove)
            {
                return;
            }
            else
            {
                ScreamerObject.transform.position = Vector3.MoveTowards(ScreamerObject.transform.position, player.position, speed * Time.deltaTime);
            }
        }
        else
        {
            return;
        }
      
    }
    private void FixedUpdate()
    {
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
            return;
        }
        var goihh = player.GetComponent<PlayerQuest>().QuestComplete;
        if (goihh == true)
        {
            audioSource.Stop();
            isMove = false;
        }
    }
    private void UpdateRotation()
    {
        if (IsRotateToMax)
        {
            ScreamerObject.transform.eulerAngles += ScreamerObject.transform.eulerAngles * Time.deltaTime * SpeedRotation;
        }
        else
        {
            ScreamerObject.transform.eulerAngles -= ScreamerObject.transform.eulerAngles * Time.deltaTime * SpeedRotation;
        }

        if (IsRotateToMax && ScreamerObject.transform.eulerAngles.y > MaxRotation)
        {
            IsRotateToMax = false;
        }
        else if (!IsRotateToMax && ScreamerObject.transform.eulerAngles.y < MinRotation)
        {
            IsRotateToMax = true;
        }
    }

    private bool IsSeePlayer()
    {
        var angle = 60f;
        var cosAngle = Mathf.Cos(angle * Mathf.Deg2Rad);
        var dir = (player.position - ScreamerObject.transform.position).normalized;
        var projectedDir = Vector3.ProjectOnPlane(dir, Vector3.up);
        var projectedScreamerForward = Vector3.ProjectOnPlane(ScreamerObject.transform.forward, Vector3.up);
        var inView = Vector3.Dot(projectedScreamerForward, projectedDir) > cosAngle;
        return inView;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            audioSource.Play();
            StartScreamer();
        }
    }

    private void StartScreamer()
    {
        if (IsSeePlayer())
        {
            var children = ScreamerObject.transform.GetChild(0);
            children.gameObject.SetActive(true);
            isMove = true;
        }
    }

}

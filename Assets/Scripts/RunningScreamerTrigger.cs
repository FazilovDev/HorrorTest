using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunningScreamerTrigger : MonoBehaviour
{
    private RunningScreamer screamer;

    private void Start()
    {
        screamer = GetComponentInChildren<RunningScreamer>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && !screamer.IsAppear)
        {
            screamer.Appear();
        }
    }
}

using System.Collections;
using UnityEngine;

public class ScreamerTriggerZone : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            var screamerController = other.transform.GetComponentInChildren<ScreamersController>();
            screamerController.Activate();
        }
    }
}
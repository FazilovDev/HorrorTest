using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHiddenPlace : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            var playerController = other.GetComponent<PlayerController>();
            playerController.IsHidden = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            var playerController = other.GetComponent<PlayerController>();
            playerController.IsHidden = false;
        }
    }
}

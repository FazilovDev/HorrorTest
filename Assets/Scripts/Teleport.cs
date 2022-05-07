using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class Teleport : NetworkBehaviour
{
    [SerializeField] private Transform teleportPosition;

    private float cooldownTeleport = 1f;
    private float currentTime = 0f;
    private bool isCooldown = false;

    [ServerCallback]
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.root.CompareTag("Player") && !isCooldown)
        {
            var player = other.transform;
            var a = player.GetComponent<NetworkTransform>();
            a.RpcTeleport(teleportPosition.position);
            isCooldown = true;
            Debug.Break();
        }
    }

    private void Update()
    {
        if (isCooldown)
        {
            currentTime += Time.deltaTime;
            if (currentTime >= cooldownTeleport)
            {
                isCooldown = false;
                currentTime = 0f;
            }
        }
    }
}

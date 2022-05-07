using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class InteractWithBattery : NetworkBehaviour
{
    private Raycaster raycaster;
    private bool isSendMessage;
    private PlayerController player;

    private void Start()
    {
        raycaster = GetComponentInChildren<Raycaster>();
        player = GetComponent<PlayerController>();
    }


    private void FixedUpdate()
    {
        if (!isLocalPlayer)
        {
            return;
        }

        var isBattery = raycaster.Hited && raycaster.Target != null && raycaster.Target.CompareTag("Battery");
        if (isBattery)
        {
            isSendMessage = true;
            SignalSystem<MessageView>.Pub(new MessageView() { Message = "Нажмите E, чтобы подобрать батарейку" });
        }
        else if (isSendMessage)
        {
            SignalSystem<MessageView>.Pub(new MessageView() { Message = "" });
            isSendMessage = false;
        }

        if (Input.GetKeyDown(KeyCode.E) && isBattery)
        {
            var value = raycaster.Target.transform.root.GetComponent<PickupBattery>();
            AddBattery(value.chargeValue);

            player.Inventory.AddItem(value.Item);
            Destroy(value.gameObject);
        }
    }

    [Command]
    public void AddBattery(float value)
    {
        var currentPlayer = GetComponent<PlayerController>();
        if (currentPlayer.battery < currentPlayer.batteryMax)
            currentPlayer.battery += value;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class InteractWithMapPlan : NetworkBehaviour
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

        var isMapPlan = raycaster.Hited && raycaster.Target != null && raycaster.Target.CompareTag("MapPlan");
        if (isMapPlan)
        {
            isSendMessage = true;
            SignalSystem<MessageView>.Pub(new MessageView() { Message = "Нажмите E, чтобы подобрать чертёж" });
        }
        else if (isSendMessage)
        {
            SignalSystem<MessageView>.Pub(new MessageView() { Message = "" });
            isSendMessage = false;
        }

        if (Input.GetKeyDown(KeyCode.E) && isMapPlan)
        {
            var value = raycaster.Target.transform.root.GetComponent<PickupMapPlan>();

            player.Inventory.AddItem(value.Item);
            Destroy(value.gameObject);
        }
    }

    [Command]
    public void AddStamina(float value)
    {
        var currentPlayer = GetComponent<PlayerController>();
        currentPlayer.UpdateMaxStamina(value);
    }
}

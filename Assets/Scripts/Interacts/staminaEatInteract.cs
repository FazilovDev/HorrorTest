using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class staminaEatInteract : NetworkBehaviour
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

        var isEnergy = raycaster.Hited && raycaster.Target != null && raycaster.Target.CompareTag("energy");
        if (isEnergy)
        {
            isSendMessage = true;
            SignalSystem<MessageView>.Pub(new MessageView() { Message = "Нажмите E, чтобы подобрать энергетик" });
        }
        else if (isSendMessage)
        {
            SignalSystem<MessageView>.Pub(new MessageView() { Message = "" });
            isSendMessage = false;
        }

        if (Input.GetKeyDown(KeyCode.E) && isEnergy)
        {
            var value = raycaster.Target.transform.root.GetComponent<PickupEnergy>();
            AddStamina(value.chargeValue);

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
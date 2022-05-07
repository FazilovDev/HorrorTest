using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class PickupBattery : NetworkBehaviour
{
    [Header("Battery System Settings")]
    public float chargeValue;

    public Item Item;

    public void DestroyBattery()
    {
        Destroy(gameObject);
    }
}

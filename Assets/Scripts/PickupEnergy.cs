using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class PickupEnergy : NetworkBehaviour
{
    [Header("Energy System Settings")]
    public float chargeValue;

    public Item Item;

    public void DestroyEnergy()
    {
        Destroy(gameObject);
    }
}

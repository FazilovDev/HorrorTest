using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class PickupMapPlan : NetworkBehaviour
{
    [Header("PlanMap System Settings")]

    public Item Item;

    public void DestroyMapPlan()
    {
        Destroy(gameObject);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Inventory : MonoBehaviour
{
    public int CountSlots = 3;
    public List<InventorySlot> Slots = new List<InventorySlot>();

    public void AddItem(Item item)
    {
        var slot = Slots.FirstOrDefault(t => t.ItemSlot.Type == item.Type);
        if (slot == null)
        {
            slot = new InventorySlot() { ItemSlot = item, CountItems = 1 };
            Slots.Add(slot);
        }
        else
        {
            slot.CountItems += 1;
        }

        SignalSystem<ItemSignal>.Pub(new ItemSignal() { Slot = slot });
    }
}

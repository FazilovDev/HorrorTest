using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

[System.Serializable]
public class InventoryShowSlot
{
    public ItemType Type;
    public Image Image;
    public Text CountItem;
}

public class ItemSignal : ISignal
{
    public InventorySlot Slot;
}

public class InventoryShow : MonoBehaviour
{
    public List<InventoryShowSlot> Slots;
    public GameObject InventoryMenu;

    public bool IsActive;

    private void Start()
    {
        IsActive = false;

        SignalSystem<ItemSignal>.Sub(OnItemSignal);
    }

    private void OnDestroy()
    {
        SignalSystem<ItemSignal>.UnSub(OnItemSignal);
    }

    private void OnItemSignal(ItemSignal signal)
    {
        AddSlot(signal.Slot);
    }

    public void AddSlot(InventorySlot slot)
    {
        var type = slot.ItemSlot.Type;
        var currentSlot = Slots.FirstOrDefault(t => t.Type == type);

        currentSlot.Image.sprite = slot.ItemSlot.Sprite;
        currentSlot.CountItem.text = slot.CountItems.ToString();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            IsActive = !IsActive;
            InventoryMenu.SetActive(IsActive);
        }

        DataManager.GetItemByType(ItemType.Battery);
    }
}

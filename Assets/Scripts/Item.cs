using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    None = -1,
    Battery = 0,
    MedKit,
    Stamina,
    Money,
    MapPlan
}

[CreateAssetMenu(fileName = "Item", menuName = "Data/Item")]
public class Item : BaseData
{
    public ItemType Type;
    public Sprite Sprite;
    public GameObject Prefab;

    public override int DataKey => (int)Type;
}

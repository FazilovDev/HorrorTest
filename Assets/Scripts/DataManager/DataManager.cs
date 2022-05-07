using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
[DefaultExecutionOrder(-1000)]
public partial class DataManager : MonoBehaviour
{
    private static DataManager instance;

    private readonly Dictionary<System.Type, Dictionary<int, BaseData>> items = new Dictionary<System.Type, Dictionary<int, BaseData>>();

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogErrorFormat("There's another {0}", this.GetType().Name);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);

        LoadItems();
    }

    public static T GetData<T>(int key) where T : BaseData
    {
        if (instance.items.TryGetValue(typeof(T), out var group))
        {
            if (group.TryGetValue(key, out var data))
            {
                return (T)data;
            }
        }

        return default(T);
    }

    public static T[] GetAll<T>() where T : BaseData
    {
        if (instance.items.TryGetValue(typeof(T), out var group))
        {
            return group.Values.Cast<T>().ToArray();
        }

        return new T[0];
    }

    private void LoadItems()
    {
        var res = Resources.LoadAll<BaseData>("");
        for (int i = 0; i < res.Length; i++)
        {
            var current = res[i];
            var type = current.GetType();
            if (!items.ContainsKey(type))
            {
                items.Add(type, new Dictionary<int, BaseData>());
            }

            try
            {
                items[type].Add(current.DataKey, current);
            }
            catch
            {
                Debug.LogErrorFormat("Key is already added: {0}", current.GetType());
            }
        }
    }

    public static List<Item> GetItems()
    {
        return GetAll<Item>().ToList();
    }

    public static Item GetItemByType(ItemType type)
    {
        return GetAll<Item>().FirstOrDefault(t => t.Type == type);
    }
}

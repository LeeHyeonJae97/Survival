using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemFactory
{
    public static int Count => Dic.Count;

    private static Dictionary<int, ItemSO> Dic
    {
        get
        {
            if (_dic == null)
            {
                var items = Resources.LoadAll<ItemSO>("Item");

                _dic = new Dictionary<int, ItemSO>();
                for (int i = 0; i < items.Length; i++)
                {
                    _dic.Add(items[i].Id, items[i]);
                }
            }
            return _dic;
        }
    }
    private static Dictionary<int, ItemSO> _dic;

    public static ItemSO Get(int id)
    {
        if (!Dic.TryGetValue(id, out ItemSO item))
        {
            Debug.LogError($"There's no Item : {id}");
        }
        return item;
    }
}

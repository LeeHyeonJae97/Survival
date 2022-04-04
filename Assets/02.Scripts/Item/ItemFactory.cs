using System.Collections;
using System.Collections.Generic;
using System.Linq;
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

    public static ItemSO[] GetRandom(int count)
    {
        if (count < 1)
        {
            Debug.LogError("count should more than zero");
            return null;
        }

        // TODO :
        // upgrade to 'Fisher-Yates shuffle'

        System.Random random = new System.Random();
        return _dic.Values.OrderBy(x => random.Next()).Take(count).ToArray();
    }
}

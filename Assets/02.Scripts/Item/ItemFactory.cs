using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ItemFactory
{
    public static int Count => Infos.Length;

    private static Item[] List
    {
        get
        {
            if (_list == null)
            {
                // load info scriptableobjects
                var infos = Infos.OrderBy((x) => x.Id).ToArray();

                // load saved data
                JsonFileSystem<ItemData>.Load(ItemData.DIR_PATH, ItemData.FILE_PATH, out ItemData data);

                // initialize data with info scriptableobjects
                for (int i = 0; i < data.Items.Length; i++) data.Items[i].Init(infos[i]);

                // cache the data
                _list = data.Items;
            }
            return _list;
        }
    }
    private static Dictionary<int, Item> Dic
    {
        get
        {
            if (_dic == null)
            {
                _dic = new Dictionary<int, Item>();

                for (int i = 0; i < List.Length; i++)
                {
                    _dic.Add(List[i].Info.Id, List[i]);
                }
            }
            return _dic;
        }
    }
    private static ItemSO[] Infos
    {
        get
        {
            if (_infos == null) _infos = Resources.LoadAll<ItemSO>("ItemSO");
            return _infos;
        }
    }

    private static Item[] _list;
    private static Dictionary<int, Item> _dic;
    private static ItemSO[] _infos;

    public static Item Get(int id)
    {
        if (!Dic.TryGetValue(id, out Item item))
        {
            Debug.LogError($"There's no Item : {id}");
        }
        return item;
    }

    public static Item[] GetAll()
    {
        return List;
    }

    public static Item[] GetRandom(int count)
    {
        if (count < 1)
        {
            Debug.LogError("count should more than zero");
            return null;
        }

        // TODO :
        // upgrade to 'Fisher-Yates shuffle'

        count = Mathf.Min(count, List.Length);

        System.Random random = new System.Random();
        return List.OrderBy(x => random.Next()).Take(count).ToArray();
    }
}

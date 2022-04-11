using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PotionFactory
{
    public static int Count { get; private set; }

    private static Potion[] List
    {
        get
        {
            if (_list == null)
            {
                // load info scriptableobjects
                var infos = Resources.LoadAll<PotionSO>("PotionSO");
                infos = infos.OrderBy((x) => x.Id).ToArray();

                // save the count
                Count = infos.Length;

                // load saved data
                JsonFileSystem<PotionData>.Load(PotionData.DIR_PATH, PotionData.FILE_PATH, out PotionData data);

                // initialize data with info scriptableobjects
                for (int i = 0; i < data.Potions.Length; i++) data.Potions[i].Init(infos[i]);

                // cache the data
                _list = data.Potions;
            }
            return _list;
        }
    }

    private static Dictionary<int, Potion> Dic
    {
        get
        {
            if (_dic == null)
            {
                _dic = new Dictionary<int, Potion>();

                for (int i = 0; i < List.Length; i++)
                {
                    _dic.Add(List[i].Info.Id, List[i]);
                }
            }
            return _dic;
        }
    }

    private static Potion[] _list;
    private static Dictionary<int, Potion> _dic;

    public static Potion Get(int id)
    {
        if (!Dic.TryGetValue(id, out Potion potion))
        {
            Debug.LogError($"There's no Item : {id}");
        }
        return potion;
    }

    public static Potion[] GetAll()
    {
        return List;
    }

    public static Potion[] GetRandom(int count)
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

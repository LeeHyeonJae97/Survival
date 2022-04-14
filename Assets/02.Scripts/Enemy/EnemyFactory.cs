using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public class EnemyFactory
{
    public static int Count => Infos.Length;

    private static Enemy[] List
    {
        get
        {
            if (_list == null)
            {
                // load info scriptableobjects
                var infos = Infos.OrderBy((x) => x.Id).ToArray();

                // load saved data
                JsonFileSystem<EnemyData>.Load(EnemyData.DIR_PATH, EnemyData.FILE_PATH, out EnemyData data);

                // initialize data with info scriptableobjects
                for (int i = 0; i < data.Enemies.Length; i++) data.Enemies[i].Init(infos[i]);

                // cache the data
                _list = data.Enemies;
            }
            return _list;
        }
    }
    private static Dictionary<int, Enemy> Dic
    {
        get
        {
            if (_dic == null)
            {
                _dic = new Dictionary<int, Enemy>();

                for (int i = 0; i < List.Length; i++)
                {
                    _dic.Add(List[i].Info.Id, List[i]);
                }
            }
            return _dic;
        }
    }
    private static EnemySO[] Infos
    {
        get
        {
            if(_infos == null) _infos = Resources.LoadAll<EnemySO>("EnemySO");
            return _infos;
        }
    }

    private static Enemy[] _list;
    private static Dictionary<int, Enemy> _dic;
    private static EnemySO[] _infos;

    public static Enemy Get(int id)
    {
        if (!Dic.TryGetValue(id, out Enemy enemy))
        {
            Debug.LogError($"There's no Item : {id}");
        }
        return enemy;
    }

    public static Enemy[] GetAll()
    {
        return List;
    }

    public static Enemy[] GetRandom(int count)
    {
        if (count < 1)
        {
            Debug.LogError("count should more than zero");
            return null;
        }

        // TODO :
        // upgrade to 'Fisher-Yates shuffle'

        System.Random random = new System.Random();
        return List.OrderBy(x => random.Next()).Take(count).ToArray();
    }
}

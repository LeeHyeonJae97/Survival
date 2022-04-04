using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyFactory
{
    public static int Count => Dic.Count;

    private static Dictionary<int, EnemySO> Dic
    {
        get
        {
            if (_dic == null)
            {
                var enemies = Resources.LoadAll<EnemySO>("Enemy");

                _dic = new Dictionary<int, EnemySO>();
                for (int i = 0; i < enemies.Length; i++)
                {
                    _dic.Add(enemies[i].Id, enemies[i]);
                }
            }
            return _dic;
        }
    }

    private static Dictionary<int, EnemySO> _dic;

    public static EnemySO Get(int id)
    {
        if (!Dic.TryGetValue(id, out EnemySO enemy))
        {
            Debug.LogError($"There's no Enemy : {id}");
        }
        return enemy;
    }

    public static EnemySO[] GetRandom(int count)
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

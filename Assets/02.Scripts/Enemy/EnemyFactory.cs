using System.Collections;
using System.Collections.Generic;
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
}

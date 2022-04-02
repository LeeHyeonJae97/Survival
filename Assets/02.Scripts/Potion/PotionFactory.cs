using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotionFactory : MonoBehaviour
{
    public static int Count => Dic.Count;

    private static Dictionary<int, PotionSO> Dic
    {
        get
        {
            if (_dic == null)
            {
                var potions = Resources.LoadAll<PotionSO>("Potion");

                _dic = new Dictionary<int, PotionSO>();
                for (int i = 0; i < potions.Length; i++)
                {
                    _dic.Add(potions[i].Id, potions[i]);
                }
            }
            return _dic;
        }
    }
    private static Dictionary<int, PotionSO> _dic;

    public static PotionSO Get(int id)
    {
        if (!Dic.TryGetValue(id, out PotionSO potion))
        {
            Debug.LogError($"There's no Item : {id}");
        }
        return potion;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum StatType { Hp, Atk, Speed, CooldownReduce }

[System.Serializable]
public class Stat
{
    public static StatInfoSO[] Infos
    {
        get
        {
            if (_infos == null)
            {
                List<StatInfoSO> infos = new List<StatInfoSO>(Resources.LoadAll<StatInfoSO>("StatInfoSO"));

                if(infos != null)
                {
                    infos.Sort((l, r) => l.Type.CompareTo(r.Type));
                    _infos = infos.ToArray();
                }
            }
            return _infos;
        }
    }

    private static StatInfoSO[] _infos;

    [field: SerializeField] public StatType Type { get; private set; }
    [field: SerializeField] public int Value { get; private set; }
}

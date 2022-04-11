using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CharacterStat", menuName = "ScriptableObject/Character/CharacterStat")]
public class CharacterStatSO : ScriptableObject
{
    [field: SerializeField] public Stat[] Stats { get; private set; }

    private void OnValidate()
    {
        int length = System.Enum.GetValues(typeof(StatType)).Length;

        if (Stats == null)
        {
            Stats = new Stat[length];
        }
        else if (Stats.Length != length)
        {
            Stat[] replace = new Stat[length];

            for (int i = 0; i < Stats.Length && i < length; i++)
            {
                replace[i] = Stats[i];
            }
            Stats = replace;
        }
    }
}

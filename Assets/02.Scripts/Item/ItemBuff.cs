using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ItemBuff
{
    [field: SerializeField] public StatType Type { get; private set; }
    [field: SerializeField] public int[] Values { get; private set; }
}

[System.Serializable]
public class ItemBuffList
{
    [field: SerializeField] public ItemBuff[] Buffs { get; private set; }

    public void OnValidate()
    {
        if (Buffs == null)
        {
            Buffs = new ItemBuff[Item.MAX_REINFORCED];
        }
        else if (Buffs.Length != Item.MAX_REINFORCED)
        {
            ItemBuff[] replace = new ItemBuff[Item.MAX_REINFORCED];

            for (int i = 0; i < Buffs.Length && i < Item.MAX_REINFORCED; i++)
            {
                replace[i] = Buffs[i];
            }
            Buffs = replace;
        }
    }
}

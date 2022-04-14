using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemBuff", menuName = "ScriptableObject/Item/ItemBuff")]
public class ItemBuffSO : ScriptableObject
{
    [field: SerializeField] public ItemBuff[] Buffs { get; private set; }

    private void OnValidate()
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

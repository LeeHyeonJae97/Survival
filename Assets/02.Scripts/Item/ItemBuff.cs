using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ItemBuff
{
    [field: SerializeField] public StatType Type { get; private set; }
    [field: SerializeField] public int[] Values { get; private set; }
}

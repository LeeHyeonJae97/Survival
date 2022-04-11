using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item
{
    public const int MAX_REINFORCED = 10;

    [field: SerializeField] public int Reinforced { get; private set; }
    [field: SerializeField] public bool Registered { get; private set; }
    public ItemSO Info { get; private set; }

    public Item()
    {
        Reinforced = 0;
        Registered = false;
    }

    public void Init(ItemSO info)
    {
        Info = info;
    }

    public void Reinforce()
    {
        Reinforced++;
    }

    public void Register()
    {
        Registered = true;
    }
}

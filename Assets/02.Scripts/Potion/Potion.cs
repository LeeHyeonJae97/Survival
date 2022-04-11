using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Potion
{
    [field: SerializeField] public bool Registered { get; private set; }
    public PotionSO Info { get; private set; }

    public Potion()
    {
        Registered = false;
    }

    public void Init(PotionSO info)
    {
        Info = info;
    }

    public void Register()
    {
        Registered = true;
    }
}

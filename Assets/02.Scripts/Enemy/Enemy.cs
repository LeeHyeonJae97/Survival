using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Enemy
{
    [field: SerializeField] public bool Registered { get; private set; }
    public EnemySO Info { get; private set; }

    public Enemy()
    {
        Registered = false;
    }

    public void Init(EnemySO info)
    {
        Info = info;
    }

    public void Register()
    {
        Registered = true;
    }
}

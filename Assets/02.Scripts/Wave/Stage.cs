using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Stage
{
    [field: SerializeField] public int ClearedIndex { get; set; }
    public bool Cleared => ClearedIndex == Info.Waves.Length;
    public StageSO Info { get; private set; }

    public Stage()
    {
        ClearedIndex = 0;
    }

    public void Init(StageSO info)
    {
        Info = info;
    }
}
